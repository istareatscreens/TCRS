using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CourseManagement;
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

    }
}
