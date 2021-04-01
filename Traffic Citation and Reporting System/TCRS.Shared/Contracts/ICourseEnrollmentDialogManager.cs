using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CourseEnrollment;

namespace TCRS.Shared.Contracts
{
    public interface ICourseEnrollmentDialogManager
    {
        Task<List<CourseEnrollmentData>> GetCourseData(CourseEnrollmentData courseEnrollmentData);
    }
}
