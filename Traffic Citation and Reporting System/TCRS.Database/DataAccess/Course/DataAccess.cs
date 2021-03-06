using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Course Management
    public partial class DataAccess
    {

        //course management
        public IEnumerable<Registration> GetRegistrationList(int course_id, string connectionString)
        {

            var sql = "SELECT * FROM (SELECT * FROM registration WHERE course_id = @course_id) as reg " +
                      "LEFT JOIN citizen ON reg.citizen_id = citizen.citizen_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = connection.Query<Registration, Citizen, Registration>(sql, (Registration, Citizen) =>
               {
                   Registration.Citizen = Citizen;
                   return Registration;
               }, new { course_id = course_id }, splitOn: "citizen_id, citizen_id");

                return rows;
            }
        }


        public IEnumerable<Course> GetUnevaluatedCourses(int school_id, string connectionString)
        {
            return SyncLoadData<Course, Course>(
                "SELECT * FROM course WHERE school_id = @school_id AND evaluated=0",
                new Course { school_id = school_id },
                connectionString
                );
        }

        public void UpdateStudentsPassedStatusInCourse(int course_id, int citizen_id, bool passed, string connectionString)
        {
            UpdateData<Registration>("UPDATE registration SET passed = @passed WHERE (citizen_id = @citizen_id) and (course_id = @course_id)", new Registration { course_id = course_id, citizen_id = citizen_id, passed = passed }, connectionString);

        }

        public void RetireCourse(int course_id, string connectionString)
        {
            UpdateData<Course>("UPDATE course SET evaluated = 1 WHERE (course_id = @course_id)", new Course { course_id = course_id }, connectionString);
        }

        public void PostCourse(Course Course, string connectionString)
        {
            SaveData<Course>(
            "INSERT INTO `Course` (`type`, `address`, `name`, `scheduled`, `price`, `description`, `title`, `instructor`, `capacity`, `citation_type_id`, `school_id`) " +
            "VALUES (@type, @address, @name, @scheduled, @price, @description, @title, @instructor, @capacity, @citation_type_id, @school_id)",
                  Course, connectionString
               );
        }

        public IEnumerable<School_Rep> GetSchoolRep(int person_id, string connectionString)
        {
            return SyncLoadData<School_Rep, Person>(
                "SELECT * FROM school_rep WHERE person_id = @person_id",
                new Person { person_id = person_id },
                connectionString
                );
        }

        public IEnumerable<Course> GetCoursesByCitationType(int citation_type_id, DateTime date, string connectionString)
        {

            var sql = @"SELECT * FROM (SELECT  course_id, type, address, name, scheduled, price, description, title, instructor, capacity, citation_type_id, school_id as course_school_id " +
                        "FROM Course WHERE citation_type_id = @citation_type_id AND scheduled > @date) as cit " +
                        "INNER JOIN School ON School.school_id = cit.course_school_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Course, School, Course>(sql, (Course, School) =>
               {
                   Course.School = School;
                   Course.school_id = School.school_id; //needed to alias school id
                   return Course;
               }, new { citation_type_id = citation_type_id, date = date }, splitOn: "course_id, school_id");

                return rows;
            }

        }

        public int GetEnrollmentNumberForCourse(int course_id, string connectionString)
        {
            return GetCount<DynamicParameters>("SELECT COUNT(*) FROM registration WHERE course_id = @course_id", new DynamicParameters(new { course_id = course_id }), connectionString);
        }

        public void UpdateCourseToFull(int course_id, string connectionString)
        {
            var sql = @"UPDATE course SET is_full = '1' WHERE (course_id = @course_id)"; //set course as resolved
            UpdateData<DynamicParameters>(sql, new DynamicParameters(new { course_id = course_id }), connectionString);
        }


        public IEnumerable<Course> GetCourseById(int course_id, string connectionString)
        {
            return SyncLoadData<Course, Course>(@"SELECT * FROM course WHERE course_id = @course_id", new Course { course_id = course_id }, connectionString);
        }
        public void RegisterCitizenInCourse(int citation_id, int course_id, int citizen_id, string connectionString)
        {
            SaveData<DynamicParameters>("INSERT INTO registration (`citizen_id`, `course_id`, `citation_id`) VALUES (@citizen_id, @course_id, @citation_id)",
               new DynamicParameters(new { citizen_id = citizen_id, course_id = course_id, citation_id = citation_id }), connectionString);
        }
    }
}
