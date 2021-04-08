using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CourseEnrollment;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Shared.Contracts.CourseManagement
{
    public interface ICourseManager
    {
        Task CreateCourse(CoursePostingData courseManagementData);
        Task<List<CourseEnrollmentData>> GetCourses(string citation_type_id);
        Task BookCourse(CourseEnrollmentBookingData bookingData);
        Task<IEnumerable<KeyValuePair<CoursePostingData, IEnumerable<StudentData>>>> GetCourseEnrollmentData();
        Task RetireCourse(CoursePostingData courseData);
        Task PassFailStudent(StudentData studentData, bool passed);
    }
}
