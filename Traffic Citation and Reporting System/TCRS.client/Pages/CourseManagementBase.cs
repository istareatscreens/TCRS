using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Client.BusyOverlay;
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

        [Inject]
        protected BusyOverlayService BusyOverlayService { get; set; }
        protected bool Switch_passed { get; set; }

        protected IEnumerable<KeyValuePair<CoursePostingData, IEnumerable<StudentData>>> CourseEnrollmentData { get; set; }
            = new List<KeyValuePair<CoursePostingData, IEnumerable<StudentData>>>();
        protected override async Task OnInitializedAsync()
        {
            await LoadCourses();
        }

        protected async Task LoadCourses()
        {
            try
            {

                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                base.OnInitialized();
                var result = await CourseManager.GetCourseEnrollmentData();
                this.CourseEnrollmentData = result;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }

        }


        protected async Task PassFailStudent(StudentData student, bool passed)
        {
            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                await CourseManager.PassFailStudent(student, passed);

                // reset the forms
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }


        protected async Task RetireCourse(CoursePostingData coursePostingData)
        {
            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                await CourseManager.RetireCourse(coursePostingData);
                // reset the forms
                await LoadCourses();
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }

    }
}
