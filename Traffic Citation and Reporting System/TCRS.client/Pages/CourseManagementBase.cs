using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CourseManagement;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Client.Pages
{
    public class CourseManagementBase : ComponentBase
    {

        [Inject]
        private ICourseManager CourseManager { get; set; }

        [Inject]
        private IUserService CurrentUser { get; set; }

        [Inject]
        ISnackbar SnackBar { get; set; }

        protected IEnumerable<KeyValuePair<CoursePostingData, IEnumerable<StudentData>>> CourseEnrollmentData { get; set; }
            = new List<KeyValuePair<CoursePostingData, IEnumerable<StudentData>>>();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                base.OnInitialized();
                var result = await CourseManager.GetCourseEnrollmentData();
                this.CourseEnrollmentData = result;
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }

        }

        protected async void PassFailStudent(StudentData student, bool passed)
        {
            try
            {
                await CourseManager.PassFailStudent(student, passed);

                // reset the forms
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }


        protected async void RetireCourse(CoursePostingData coursePostingData)
        {
            try
            {
                await CourseManager.RetireCourse(coursePostingData);

                // reset the forms
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }

    }
}
