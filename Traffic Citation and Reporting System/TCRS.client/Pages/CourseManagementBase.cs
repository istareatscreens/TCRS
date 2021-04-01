using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        public string PersonName { get; set; }
        public string PersonID { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(CourseData);

            PersonName = CurrentUser.GetFullName();
            
        }

        bool success;

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            await CourseManager.CreateCourse(CourseData);

            success = true;
            StateHasChanged();
        }


        public string Disabled { get; set; }
       
        

    }
}
