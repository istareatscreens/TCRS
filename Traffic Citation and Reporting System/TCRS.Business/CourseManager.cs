using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CourseManagement;
using TCRS.Shared.Objects.CourseEnrollment;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Business
{
    public class CourseManager : ICourseManager
    {
        private readonly IPersistenceService _api;
        public CourseManager(IPersistenceService api)
        {
            _api = api;
        }

        public async Task CreateCourse(CourseManagementData courseManagementData)
        {
            await _api.PostAsync<CourseManagementData>(courseManagementData);
        }

        public async Task<List<CourseEnrollmentData>> GetCourses(string citation_type_id)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("citation_type_id", citation_type_id));

            var data = await _api.GetAsync<CourseEnrollmentData>(parameters);
            return data.ToList();
        }

        public async Task BookCourse(CourseEnrollmentBookingData bookingData)
        {
            await _api.PostAsync<CourseEnrollmentBookingData>(bookingData);
        }

    }
}