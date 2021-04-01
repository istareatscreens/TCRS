using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.CourseEnrollment;

namespace TCRS.Business
{
    class CourseEnrollmentDialogManager : ICourseEnrollmentDialogManager
    {
        private readonly IPersistenceService _api;
        public CourseEnrollmentDialogManager(IPersistenceService _api)
        {
            _api = _api;
        }
        public async Task<List<CourseEnrollmentData>> GetCourseData(CourseEnrollmentData courseEnrollmentData)
        {
            var ced = await _api.GetAsync<CourseEnrollmentData>();
            return ced.ToList();
        }
    }
}
