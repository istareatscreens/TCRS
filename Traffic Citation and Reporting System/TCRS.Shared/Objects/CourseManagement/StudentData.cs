using System;

namespace TCRS.Shared.Objects.CourseManagement
{
    public class StudentData
    {
        public int course_id { get; set; }
        public int citizen_id { get; set; }
        public bool passed { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime dob { get; set; }
    }
}
