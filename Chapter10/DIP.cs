using System.Collections.Generic;
using System.Linq;
using EFSchools.Englishtown.Oboe.ClassCategoryGroup;
using EFSchools.Englishtown.Oboe.Enums;
using EFSchools.Englishtown.Oboe.ScheduledClass;
using EFSchools.Englishtown.Oboe2.Lookups;

namespace EFSchools.Englishtown.Oboe.BookingRuleFramework.Booking.Rules.Mini
{
	// doing too much in one method
	// low level coupling - ClassCategoryGroupCacheSvc.Instance.GetClassCategoryGroupInfoByClassCategory
	// weekly count, monthly count etc. I saw smell of violation of OCP.

    [MiniBookingRuleCategory(MiniClassCategorys = new ClassCategory[] { ClassCategory.CW, ClassCategory.LC, ClassCategory.WS, ClassCategory.F2F })]
    class CommonBookingLimitBookingRule : IMiniBookingRule
    {
        protected CommonBookingLimitBookingRule()
        {
        }

        #region IBookingRule Members

        public List<BookingStatus> RemoveInvalidBookingStatus(StudentBookingContext studentBookingContext, ClassBookingContext classBookingContext)
        {
            List<BookingStatus> result = null;

            ClassCategory classCategory = (ClassCategory)classBookingContext.ScheduledClassInfo.ClassCategory_id;

            //predefined class category group info list
            var groupInfoList = ClassCategoryGroupCacheSvc.Instance.GetClassCategoryGroupInfoByClassCategory(classCategory);

            foreach (var groupInfo in groupInfoList)
            {
                var bookingLimitLkpList = BookingLimitLkpList.LoadBookingLimitLkp(groupInfo.ClassCategoryGroup_id, studentBookingContext.Student.Product_id.GetValueOrDefault());

                if (bookingLimitLkpList != null && bookingLimitLkpList.Count > 0)
                {
                    //check booking limit

                    var bookingLimitLkp = bookingLimitLkpList.First();

                    // PremiumV1's weekcount doesn't include no F2F standby classes
                    int weekCount = studentBookingContext.StudentSelectedWeekBooking.BookingInfoList.Count(
                        info => groupInfo.ClassCategory_ids.Any(id => id == ScheduledClassCacheSvc.Instance.LoadByScheduledClass_id(info.ScheduledClass_id).ClassCategory_id)
                        && (info.BookingStatus_id == (short)BookingStatus.Booked
                            || (info.BookingStatus_id == (short)BookingStatus.Standby
                                && (classCategory == ClassCategory.F2F
                                    || studentBookingContext.Student.Product_id.Value != (short)ProductPackage_Ids.PremiumV1
                                    )
                                )
                            || info.BookingStatus_id == (short)BookingStatus.Checkin
                            || info.BookingStatus_id == (short)BookingStatus.NoShow
                            || info.BookingStatus_id == (short)BookingStatus.Waiting
                            || info.BookingStatus_id == (short)BookingStatus.TentativelyBooked
                        )
                        );

                    int monthCount = studentBookingContext.StudentSelectedMonthBooking.BookingInfoList.Count(
                        info => groupInfo.ClassCategory_ids.Any(id => id == ScheduledClassCacheSvc.Instance.LoadByScheduledClass_id(info.ScheduledClass_id).ClassCategory_id)
                        && (info.BookingStatus_id == (short)BookingStatus.Booked
                            || info.BookingStatus_id == (short)BookingStatus.Standby
                            || info.BookingStatus_id == (short)BookingStatus.Checkin
                            || info.BookingStatus_id == (short)BookingStatus.NoShow
                            || info.BookingStatus_id == (short)BookingStatus.Waiting
                            || info.BookingStatus_id == (short)BookingStatus.TentativelyBooked
                        )
                        );

                    int openCount = studentBookingContext.StudentBookingInfo.BookingInfoList.Count(
                        info => groupInfo.ClassCategory_ids.Any(id => id == ScheduledClassCacheSvc.Instance.LoadByScheduledClass_id(info.ScheduledClass_id).ClassCategory_id)
                        && (info.BookingStatus_id == (short)BookingStatus.Booked
                            || info.BookingStatus_id == (short)BookingStatus.Waiting
                            || info.BookingStatus_id == (short)BookingStatus.TentativelyBooked
                            || info.BookingStatus_id == (short)BookingStatus.Standby
                        )
                        );

                    if (bookingLimitLkp.WeekLimit.HasValue && 0 <= bookingLimitLkp.WeekLimit.Value && bookingLimitLkp.WeekLimit.Value <= weekCount)
                    {
                        // PremiumV1 can standy WS,LC,CW class when reach weeklimit.
                        if (studentBookingContext.Student.Product_id.Value == (short)ProductPackage_Ids.PremiumV1
                            && classCategory != ClassCategory.F2F)
                        {
                            return new List<BookingStatus>() { BookingStatus.Booked, BookingStatus.Waiting, BookingStatus.TentativelyBooked };
                        }
                        else
                        {
                            return new List<BookingStatus>() { BookingStatus.All };
                        }
                    }

                    if (bookingLimitLkp.MonthLimit.HasValue && 0 <= bookingLimitLkp.MonthLimit.Value && bookingLimitLkp.MonthLimit.Value <= monthCount)
                    {
                        return new List<BookingStatus>() { BookingStatus.All };
                    }

                    if (bookingLimitLkp.OpenLimit.HasValue && 0 <= bookingLimitLkp.OpenLimit.Value && bookingLimitLkp.OpenLimit.Value <= openCount)
                    {
                        return new List<BookingStatus>() { BookingStatus.All };
                    }
                }
            }

            return result;
        }

        #endregion

    }
}

