using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Shared.Contracts.CourseManagement;

namespace TCRS.Client.Pages
{
    public class CourseManagementBase : ComponentBase
    {
        //protected CourseManagementData CourseData { get; set; } = new CourseManagementData();

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //EditContext = new EditContext(CourseData);

        }

        bool success;

        [Inject]
        private ICourseManager CourseManager { get; set; }

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            //CourseManager.CreateCourse(CourseData);

            success = true;
            StateHasChanged();
        }

        public string Disabled { get; set; }
        public string GetPersonName { get; set; } = "Instructor Name Here";

        //protected CourseManagementDisplayData data = new CourseManagementDisplayData();
    }
}
