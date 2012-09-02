using System;
using System.Data.SqlClient;
using System.Linq;
using EF.Frameworks.Common.ConfigurationEF;
using EF.Frameworks.Common.DataEF.SqlClientEF;
using EF.Frameworks.Common.FactoryEF;
using EFSchools.Englishtown.Oboe.Booking.Models;
using EFSchools.Englishtown.Oboe.Booking.Sprocs;
using EFSchools.Englishtown.Oboe.Coupon.Models;
using EFSchools.Englishtown.Oboe.Coupon.Sprocs;
using EFSchools.Englishtown.Oboe.ScheduledClass;
using EFSchools.Englishtown.Oboe.Student.Models;
using EFSchools.Englishtown.Oboe.Student.Sprocs;
using EFSchools.Englishtown.Oboe.ScheduledClass.Models;
using System.Collections.Generic;

// Doing too much in one method. Divide it into serveral steps
// After that, we find cohesion of class break coz of private variable is only useful to one function.
// It sound we need to move these functions to its own Mgr class
// Take a look at these class, we find they are actually doing something we have somewhere else. We are repeating ourselves.

namespace EFSchools.Englishtown.Oboe.BookingRuleContext
{
    class BookingContextMgr : ISingleton
    {
        private static readonly IConfigurationContext _studentContext = new ConfigurationContext(ConfigurationContextNamespace.STUDENT);
        private static readonly IConfigurationContext _couponContext = new ConfigurationContext(ConfigurationContextNamespace.COUPON);
        private static readonly IConfigurationContext _bookingContext = new ConfigurationContext(ConfigurationContextNamespace.BOOKING);
        private static readonly IConfigurationContext _scheduledClassContext = new ConfigurationContext(ConfigurationContextNamespace.SCHEDULEDCLASS);

        protected BookingContextMgr() { }

        public static readonly BookingContextMgr Instance = Factory<BookingContextMgr>.Create();

        public BookingContextInfo LoadBookingContextInfo(ScheduledClassInfoList classesInfoList, int student_id, DateTime classUtcDate, DateTime studentBookingBeginUtcDate)
        {
            BookingContextInfo result = new BookingContextInfo();

            using (SqlConnectionManager cm = new SqlConnectionManager())
            {
                // 
                //  Load Student related data
                //

                using (var cmd = new Student_Load_p())
                {
                    cmd.Parameters.Student_id = student_id;

                    using (SqlDataReader reader = cm.ExecuteReader(cmd, _studentContext))
                    {
                        result.Student = StudentInfoAssembler.CreateList(reader).First();
                    }
                }

                using (Coupon_LoadCount_p cmd = new Coupon_LoadCount_p())
                {
                    cmd.Parameters.Student_id = student_id;

                    using (SqlDataReader reader = cm.ExecuteReader(cmd, _couponContext))
                    {
                        result.StudentCoupon = CouponCountInfoAssembler.CreateList(reader);
                    }
                }

                using (var cmd = new Booking_LoadByStudentDateRange_p())
                {
                    cmd.Parameters.Student_id = student_id;
                    cmd.Parameters.BeginDate = studentBookingBeginUtcDate;
                    cmd.Parameters.EndDate = DateTime.MaxValue;

                    using (SqlDataReader reader = cm.ExecuteReader(cmd, _bookingContext))
                    {
                        result.StudentBookingInfo = new StudentBookingInfoList(BookingInfoAssembler.CreateList(reader));
                    }
                }

                // 
                //  Load scheduled class related data
                //
                result.ClassesInfoList = classesInfoList;
                result.BookingInfoListForClasses = new System.Collections.Generic.List<ScheduledClassBookingInfoList>();

                foreach (var c in result.ClassesInfoList.ScheduledClass)
                {
                    using (Booking_LoadByScheduledClass_id_p cmd = new Booking_LoadByScheduledClass_id_p())
                    {
                        cmd.Parameters.ScheduledClass_id = c.ScheduledClass_id;

                        using (SqlDataReader reader = cm.ExecuteReader(cmd, _scheduledClassContext))
                        {
                            var bookingList = BookingInfoAssembler.CreateList(reader);

                            result.BookingInfoListForClasses.Add(new ScheduledClassBookingInfoList(c.ScheduledClass_id, bookingList));
                        }
                    }
                }
            }

            return result;
        }

        public BookingContextInfo LoadBookingContextInfo(ScheduledClassInfo classInfo, int student_id, DateTime classUtcDate, DateTime studentBookingBeginUtcDate)
        {
            return this.LoadBookingContextInfo(new ScheduledClassInfoList(new List<ScheduledClassInfo> { classInfo }),
                student_id, classUtcDate, studentBookingBeginUtcDate);
        }
    }
}

