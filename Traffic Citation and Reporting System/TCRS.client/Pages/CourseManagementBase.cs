using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CourseManagement;
using TCRS.Shared.Objects.CourseManagement;

namespace TCRS.Client.Pages
{
    public class CourseManagementBase : ComponentBase
    {
        protected CourseManagementData CourseData { get; set; } = new CourseManagementData();

        protected EditContext EditContext { get; set; }

        [Inject]
        private ICourseManager CourseManager { get; set; }

        [Inject]
        private IUserService CurrentUser { get; set; }

        [Inject]
        ISnackbar SnackBar { get; set; }

        public string PersonName { get; set; }
        public string PersonID { get; set; }

        // TEMP DATA
        public Type typeValue { get; set; }

        public enum Type
        {
            RunningRed,
            SeatBeltViolation,
            FailureToFollowRightOfWay,
            FailureToSignalWhenChangingLanes,
            Speeding,
            DrivingOverAMedian,
            DrivingIllegally,
            RunningOverAPedestrianLane,
            DrivingPastASchoolBuswhenUnloadingLoading,
            FailureToDriveWithinASpecifiedLane
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(CourseData);
        }

        public DateTime? dateSelect = DateTime.Today;
        MudDatePicker _picker;

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

                await CourseManager.CreateCourse(CourseData);

                // reset the forms
                CourseData = new CourseManagementData();
                dateSelect = DateTime.Today;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }

    }
}