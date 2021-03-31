using System.Threading.Tasks;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Shared.Contracts.CourseManagement
{
    public interface ICourseManager
    {
        Task CreateCourse(CourseManagementData courseManagementData);
    }
}
