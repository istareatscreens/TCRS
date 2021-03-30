using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.CourseEnrollment
{
    public class CourseEnrollmentData
    {
        // Get request
        public int course_id { get; set; }
        public DateTime scheduled { get; set; }
        public string address { get; set; }

    }
}
