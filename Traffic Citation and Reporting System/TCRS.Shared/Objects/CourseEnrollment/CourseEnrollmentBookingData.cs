using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.CourseEnrollment
{
    public class CourseEnrollmentBookingData
    {
        // Post is course_id, citation_id, citizen_id
        public int course_id { get; set; }
        public int citation_id { get; set; }
        public int citizen_id { get; set; }
    }
}
