using System.Collections.Generic;

namespace TCRS.Shared.Objects.CourseManagement
{
    public class CourseManagementData
    {
        public IEnumerable<KeyValuePair<CoursePostingData, IEnumerable<StudentData>>> CourseEnrollmentData { get; set; }
    }
}
