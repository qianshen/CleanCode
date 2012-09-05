
using System.Collections.Generic;
using EFSchools.Englishtown.Oboe.Enums;
using EFSchools.Englishtown.Oboe.BookingRuleFramework.Interfaces;
using System.ComponentModel.Composition;

namespace EFSchools.Englishtown.Oboe.BookingRuleFramework.Rules
{
	#region Note
	// Adding new booking rule is good open close to new rules
	// Adding interface support later is the same as adding functions. Breaking OCP unless these interfaces are used for grouping only.
	// I see switch. A bad smell if logic change, we must change the codes which affects all types.
	#endregion

    /// <summary>
    /// Check open/weekly/monthly booking limit for LC + CW 
    /// </summary>
    [MiniBookingRuleCategory(MiniClassCategorys = new ClassCategory[] { ClassCategory.CW, ClassCategory.LC, ClassCategory.WS, ClassCategory.F2F })]
    [SmartBookingRuleCategory(SmartClassCategorys = new ClassCategory[] { ClassCategory.CW, ClassCategory.LC, ClassCategory.WS, ClassCategory.F2F })]
    class CommonLCCWLimitBookingRule : IMiniBookingRule, ISmartBookingRule
    {
        protected CommonLCCWLimitBookingRule() { }

        List<BookingStatus> IBookingRule.RemoveInvalidBookingStatus(StudentBookingContext studentBookingContext, ClassBookingContext classBookingContext)
        {
            List<BookingStatus> result = null;

            switch ((ClassCategory)classBookingContext.ScheduledClassInfo.ClassCategory_id)
            {
                case ClassCategory.F2F:
                    break;
                case ClassCategory.WS:
                    break;
                case ClassCategory.LC:
                case ClassCategory.CW:
                    //if (studentBookingContext.StuentProductInfo.ProductID == ProductPackage_Ids.AlumniClub)
                    //{
                    //    if (studentBookingContext.BookingSummary.LCCWQtyPerMonth >= studentBookingContext.StuentProductInfo.LCCW_PerMonth
                    //    || studentBookingContext.BookingSummary.LCCWQtyPerWeek >= studentBookingContext.StuentProductInfo.LCCW_PerWeek)
                    //    {
                    //        result = new List<BookingStatus>() { BookingStatus.All };
                    //    }
                    //    else if (studentBookingContext.BookingSummary.LGQtyPerOpen >= studentBookingContext.StuentProductInfo.LG_PerTime)
                    //    {
                    //        result = new List<BookingStatus>() { BookingStatus.Booked, BookingStatus.Waiting };
                    //    }
                    //}

                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
