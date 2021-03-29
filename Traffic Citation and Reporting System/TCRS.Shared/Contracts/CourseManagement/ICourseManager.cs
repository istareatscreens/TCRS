using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Shared.Contracts.CourseManagement
{
    public interface ICourseManager
    {
        public void CreateCourse(CourseManagementData courseManagementData);
    }
}
