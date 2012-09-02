using System;
using System.Collections.Generic;

using EF.Frameworks.Common.ConfigurationEF;
using EF.Frameworks.Common.DataEF.SqlClientEF;
using EF.Frameworks.Common.FactoryEF;
using EFSchools.Englishtown.Oboe.ClassRoom.Models;
using EFSchools.Englishtown.Oboe.ClassRoom.Sprocs;
using EFSchools.Englishtown.Oboe.Utilities;
using EFSchools.Englishtown.Oboe2.Lookups;

namespace EFSchools.Englishtown.Oboe.ClassRoom
{
    internal class ClassRoomMgr : ISingleton
    {
        private static readonly IConfigurationContext _context = new ConfigurationContext(ConfigurationContextNamespace.CLASSROOM);
        public static ClassRoomMgr Instance = new ClassRoomMgr();

        protected ClassRoomMgr()
        {
        }


        public bool IsAvailable(int scheduledClass_id, int classRoom_id, DateTime startDate, DateTime endDate)
        {
            using (var cm = new SqlConnectionManager())
            {
                using (var cmd = new Classroom_IsAvailable_p())
                {
                    cmd.Parameters.ScheduledClass_id = scheduledClass_id;
                    cmd.Parameters.ClassRoom_id = classRoom_id;
                    cmd.Parameters.BeginDate = startDate;
                    cmd.Parameters.EndDate = endDate;
                    cmd.Parameters.IsAvailable = false;

                    cm.ExecuteNonQuery(cmd, _context);
                    return cmd.Parameters.IsAvailable.GetValueOrDefault();
                }
            }
        }

        public bool IsAvailable(int scheduledClass_id, int classRoom_id, DateTime startDate, DateTime endDate, out string teacherName)
        {
            using (var cm = new SqlConnectionManager())
            {
                using (var cmd = new Classroom_IsAvailable_V2_p())
                {
                    cmd.Parameters.ScheduledClass_id = scheduledClass_id;
                    cmd.Parameters.ClassRoom_id = classRoom_id;
                    cmd.Parameters.BeginDate = startDate;
                    cmd.Parameters.EndDate = endDate;
                    cmd.Parameters.IsAvailable = false;
                    cmd.Parameters.TeacherName = null;

                    cm.ExecuteNonQuery(cmd, _context);

                    teacherName = cmd.Parameters.TeacherName;

                    return cmd.Parameters.IsAvailable.GetValueOrDefault();
                }
            }
        }

        public int CreateClassroom(Classroom_lkpInfo info)
        {
            if (info == null)
            {
                throw new Exception(ExceptionMessage.InputParameterNull);
            }

            using (SqlConnectionManager cm = new SqlConnectionManager())
            {
                Nullable<int> classroom_id = null;
                using(var cmd = new Classroom_lkp_Save_p())
                {
                    cmd.Parameters.IsInsert = true;

                    cmd.Parameters.Classroom_id = info.Classroom_id;
                    cmd.Parameters.School_id = info.School_id;
                    cmd.Parameters.IsDeleted = info.IsDeleted;
                    cmd.Parameters.ClassroomName = info.ClassroomName;
                    cmd.Parameters.Classroom_Blurb_id = info.Classroom_Blurb_id;
                    cmd.Parameters.DefaultPhysicalCapacity = info.DefaultPhysicalCapacity;
                    cmd.Parameters.DisplayOrder = info.DisplayOrder;
                    cmd.Parameters.Insertby = info.Insertby;
                    cmd.Parameters.Updateby = info.Updateby;
                    cmd.Parameters.IsHidden = info.IsHidden;

                    cm.ExecuteNonQuery(cmd, _context);
                    classroom_id = cmd.Parameters.Classroom_id;
                    if (!classroom_id.HasValue)
                    {
                        throw new Exception(ExceptionMessage.NoIdReturnedForCreatedObject);
                    }
                }
                return classroom_id.Value;
            }
        }

        public void UpdateClassroom(Classroom_lkpInfo info)
        {
            if (info == null)
            {
                throw new Exception(ExceptionMessage.InputParameterNull);
            }

            using (SqlConnectionManager cm = new SqlConnectionManager())
            {
                using (var cmd = new Classroom_lkp_Save_p())
                {
                    cmd.Parameters.IsInsert = false;

                    cmd.Parameters.Classroom_id = info.Classroom_id;
                    cmd.Parameters.School_id = info.School_id;
                    cmd.Parameters.IsDeleted = info.IsDeleted;
                    cmd.Parameters.ClassroomName = info.ClassroomName;
                    cmd.Parameters.Classroom_Blurb_id = info.Classroom_Blurb_id;
                    cmd.Parameters.DefaultPhysicalCapacity = info.DefaultPhysicalCapacity;
                    cmd.Parameters.DisplayOrder = info.DisplayOrder;
                    cmd.Parameters.Insertby = info.Insertby;
                    cmd.Parameters.Updateby = info.Updateby;
                    cmd.Parameters.IsHidden = info.IsHidden;

                    cm.ExecuteNonQuery(cmd, _context);
                }
            }
        }

        public List<Classroom_lkpInfo> LoadClassroomAllWithoutCache()
        {
            List<Classroom_lkpInfo> result = null;
            using(var cm = new SqlConnectionManager())
            {
                using (var cmd = new Classroom_lkp_LoadAll_p())
                {
                    using(var reader = cm.ExecuteReader(cmd,_context))
                    {
                        result = Classroom_lkpInfoAssembler.CreateList(reader);
                    }
                }
            }
            return result;
        }
    
		// what's problem here?

//        public bool ShouldHideOnUTCDate(ClassRoomLkp classRoomLkp, DateTime utcDate)
//        {
//            if(classRoomLkp.IsHidden)
//            {
//                var lastDateScheduled = GetLastDateClassroomScheduled(classRoomLkp.Classroom_id);
//                if(lastDateScheduled!=null)
//                    return utcDate >= GetNextMonday(lastDateScheduled.Value.ConvertFromUTCToSchool(classRoomLkp.School_id));
//                else
//                    return true;
//            }
//
//            return false;
//        }

        private DateTime? GetLastDateClassroomScheduled(int classRoomId)
        {
            using (var cm = new SqlConnectionManager())
            {
                using (var cmd = new Classroom_GetLastDateScheduled_p())
                {
                    cmd.Parameters.ClassRoom_id = classRoomId;
                    cmd.Parameters.LastDate = null;

                    cm.ExecuteNonQuery(cmd, _context);
                    
                    return cmd.Parameters.LastDate;
                }
            }
        }

        private DateTime GetNextMonday(DateTime theDate)
        {
            while (true)
            {
                theDate = theDate.AddDays(1);
                if (theDate.DayOfWeek == DayOfWeek.Monday)
                {
                    return theDate;
                }
            }
        }
    }
}
