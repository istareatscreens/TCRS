
using System;

namespace TCRS.Shared.Objects.CourseManagement
{
    public class CourseManagementData
    {
        // Get request, get object
        // Parameters(JWT)
        public int course_id { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public DateTime scheduled { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string instructor { get; set; }
        public int capacity { get; set; }
        public int citation_type_id { get; set; }
        public int school_id { get; set; }
    }
}
