﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CourseEnrollment;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Shared.Contracts.CourseManagement
{
    public interface ICourseManager
    {
        Task CreateCourse(CourseManagementData courseManagementData);
        Task<List<CourseEnrollmentData>> GetCourses(string citation_type_id);
    }
}
