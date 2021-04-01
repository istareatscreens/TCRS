using System;
using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Course Management
    public partial interface IDataAccess
    {
        void RegisterCitizenInCourse(int citation_id, int course_id, int citizen_id, string connectionString);
        void PostCourse(Course Course, string connectionString);
        IEnumerable<Course> GetCourseById(int course_id, string connectionString);
        IEnumerable<School_Rep> GetSchoolRep(int person_id, string connectionString);
        IEnumerable<Course> GetCoursesByCitationType(int citation_type_id, DateTime date, string connectionString);
        int GetEnrollmentNumberForCourse(int course_id, string connectionString);
        void UpdateCourseToFull(int course_id, string connectionString);

    }
}
