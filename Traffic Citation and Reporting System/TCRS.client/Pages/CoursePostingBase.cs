using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using TCRS.Client.BusyOverlay;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CourseManagement;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Client.Pages
{
    public class CoursePostingBase : ComponentBase
    {
        protected CoursePostingData CourseData { get; set; } = new CoursePostingData();

        protected EditContext EditContext { get; set; }

        [Inject]
        private ICourseManager CourseManager { get; set; }

        [Inject]
        private IUserService CurrentUser { get; set; }

        [Inject]
        ISnackbar SnackBar { get; set; }

        [Inject]
        protected BusyOverlayService BusyOverlayService { get; set; }

        public string PersonName { get; set; }
        public string PersonID { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(CourseData);
        }

        public DateTime? dateSelect = DateTime.Today;

        protected async void OnValidSubmit(EditContext context)
        {
            try
            {
                if (!EditContext.Validate())
                {
                    // Print out invalid input message
                    return;
                }

                //Convert type to int
                CourseData.scheduled = (DateTime)dateSelect;
                CourseData.citation_type_id = (int)CourseData.CitizenCitationType;

                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                await CourseManager.CreateCourse(CourseData);

                // reset the forms
                CourseData = new CoursePostingData();
                dateSelect = DateTime.Today;
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
