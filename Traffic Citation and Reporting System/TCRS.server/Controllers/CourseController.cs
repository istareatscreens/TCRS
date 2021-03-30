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

                _db.PostCourse(new Course
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
                }, _databaseContext.Server);
            }
            catch
            {
                return BadRequest("Failed to post object, potential database issue");
            }

            return Accepted("Course Posted");
        }

        [HttpGet("GetCourses")]
        public ActionResult<IEnumerable<CourseEnrollmentData>> GetCourses([FromQuery] int citation_type_id)
        {
            try
            {
                var courseList = _db.GetCoursesByCitationType(citation_type_id, _databaseContext.Server).ToList();
                if (courseList == null)
                {
                    throw new NullReferenceException("No Courses Available");
                }
                return courseList.Select(course =>
                {
                    //TODO: Check Capacity
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
            try
            {
                var citizenList = _db.GetCitizenById(bookingData.citizen_id, _databaseContext.Server);
                var citationList = _db.GetCitationByNumber(bookingData.citation_number, _databaseContext.Server);
                var courseList = _db.GetCourseById(bookingData.course_id, _databaseContext.Server);

                //Validate if parameters passed are valid
                if (citationList != null && citizenList != null && courseList != null)
                {
                    return BadRequest("Invalid Registration Details");
                }

                //unpack parameters
                var citizen = IEnumerableHandler.UnpackIEnumerable<Citizen>(citizenList);
                var citation = IEnumerableHandler.UnpackIEnumerable<Citation>(citationList);
                var course = IEnumerableHandler.UnpackIEnumerable<Course>(courseList);

                //TODO Write endpoint to check if citation has been resolved, and if Citizen owns citation and if citizen is already registered
                //Check if request is valid
                if (!citation.Citation_Type.training_eligable && citation.citation_type_id == course.citation_type_id)
                {
                    return BadRequest("Invalid Registration Details");
                }

                _db.RegisterCitizenInCourse(citizen.citizen_id, citation.citation_id, course.course_id, _databaseContext.Server);

            }
            catch
            {
                return BadRequest("Cannot connect to database");
            }

            return Accepted("Successfully registered for course");

        }

    }



}
