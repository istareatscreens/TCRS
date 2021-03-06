
using System;
using TCRS.Shared.Enums;

namespace TCRS.Shared.Objects.CourseManagement
{
    public class CoursePostingData
    {
        // Get request, get object
        // Parameters(JWT)
        public int course_id { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public DateTime scheduled { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string instructor { get; set; }
        public int capacity { get; set; }
        public int citation_type_id { get; set; } = (int)CitizenCitationTypes.Speeding;
        public CitizenCitationTypes CitizenCitationType { get; set; } = CitizenCitationTypes.Speeding;
        
        public string GetCourseNameAndDate()
        {
            return $"Course Name: {name}, Course Date: {scheduled.Day}/{scheduled.Month}/{scheduled.Year}";
        }

    }
}
