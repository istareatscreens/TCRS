using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Shared.Contracts.CourseManagement
{
    public interface ICourseManager
    {
        public void CreateCourse(CourseManagementData courseManagementData);
    }
}
