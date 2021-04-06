using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Helper;
using TCRS.Shared.Objects.Auth;
using TCRS.Shared.Objects.CourseEnrollment;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public CourseController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpPost("SubmitCourse")]
        public ActionResult PostCourse([FromBody] CourseManagementData courseManagementData, [FromHeader] string authorization)
        {
            try
            {
                User user = new User(authorization);
                if (!user.isSchool_Rep)
                {
                    return BadRequest("Incorrect credentials");
                }

                var Course = new Course
                {
                    type = courseManagementData.type,
                    address = courseManagementData.address,
                    name = courseManagementData.name,
                    scheduled = courseManagementData.scheduled,
                    price = courseManagementData.price,
                    description = courseManagementData.description,
                    title = courseManagementData.title,
                    instructor = courseManagementData.instructor,
                    capacity = courseManagementData.capacity,
                    citation_type_id = courseManagementData.citation_type_id,
                    school_id = IEnumerableHandler.UnpackIEnumerable<School_Rep>(_db.GetSchoolRep(user.person_id, _databaseContext.Server)).school_id
                };

                _db.PostCourse(Course

                    , _databaseContext.Server);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to post object, potential database issue" + e);
            }

            return Accepted("Course Posted");
        }

        [HttpGet("GetCourses")]
        public ActionResult<IEnumerable<CourseEnrollmentData>> GetCourses([FromQuery] int citation_type_id)
        {
            try
            {
                var courseList = _db.GetCoursesByCitationType(citation_type_id, DateTime.Now, _databaseContext.Server).ToList();
                if (courseList == null)
                {
                    throw new NullReferenceException("No Courses Available");
                }

                //Return all courses that are not full, then convert to CourseEnrollmentData
                return courseList.FindAll(course => !course.is_full).Select(course =>
                  {
                      return new CourseEnrollmentData { address = course.address, course_id = course.course_id, scheduled = course.scheduled };
                  }).ToList();
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return NotFound("Unkown Error, likely database connection error");
            }
        }

        [HttpPost("EnrollInCourse")]
        public ActionResult EnrollInCourse(CourseEnrollmentBookingData bookingData)
        {
            //try
            //{
            var citationList = _db.GetCitationAllInformationByNumber(bookingData.citation_number, _databaseContext.Server);
            var courseList = _db.GetCourseById(bookingData.course_id, _databaseContext.Server);

            //Validate if parameters passed are valid
            if (citationList == null || courseList == null)
            {
                return BadRequest("Invalid Registration Details");
            }

            //unpack parameters
            var citation = IEnumerableHandler.UnpackIEnumerable<Citation>(citationList);
            var course = IEnumerableHandler.UnpackIEnumerable<Course>(courseList);

            //Check if request is valid
            if (!citation.Citation_Type.training_eligable || !(citation.citation_type_id == course.citation_type_id) || course.scheduled < DateTime.Now)
            {
                return BadRequest("Invalid Registration Details");
            }

            //check if already registered for course
            if (_db.CitationIsRegisteredToCourse(citation.citation_id, _databaseContext.Server))
            {
                return BadRequest("Invalid Parameters");
            }

            var NumberOfSpacesLeft = course.capacity - _db.GetEnrollmentNumberForCourse(course.course_id, _databaseContext.Server);
            //Check if course is already full
            if (NumberOfSpacesLeft <= 0)
            {
                return BadRequest("Course is Full");
            }

            //Check if citation is marked as resolved, if not check in the database and update citation to be resolved if it is resolved
            if (citation.is_resolved || _db.CheckIfCitationIsResolved(citation.citation_id, _databaseContext.Server))
            {
                return BadRequest("Already resolved");
            }

            if (citation.Driver_Record == null)
            {
                return BadRequest("Invalid citation type");
            }

            //Passed all checks enroll in course
            _db.RegisterCitizenInCourse(citation.citation_id, course.course_id, citation.Driver_Record.Citizen.citizen_id, _databaseContext.Server);
            if (NumberOfSpacesLeft == 1)
            {
                _db.UpdateCourseToFull(course.course_id, _databaseContext.Server);
            }

            /*
        }
        catch (Exception e)
        {
            return BadRequest("Cannot connect to database");
        }
            */

            return Accepted("Successfully registered for course");

        }

    }



}
