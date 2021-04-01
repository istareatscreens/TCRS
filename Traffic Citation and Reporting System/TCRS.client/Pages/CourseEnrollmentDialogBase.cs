using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.CourseEnrollment;

namespace TCRS.Client.Pages
{
    public class CourseEnrollmentDialogBase : ComponentBase
    {

        protected CourseEnrollmentData CourseData { get; set; } = new CourseEnrollmentData();

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected void Cancel() => MudDialog.Cancel();

        //[Inject]
        //private ICourseEnrollmentDialogManager CourseManager { get; set; }
        /*
        protected override void OnInitialized()
        {
            base.OnInitialized();

            //var data = CourseManager.GetCourseData();

        }
        */
        // TEMP DATA
        protected string[] headings = { "Course ID", "Date", "Location" };
        protected string[] rows = {
            @"1 Date1 Location1",
            @"2 Date2 Location2",
            @"3 Date3 Location3",
            @"4 Date4 Location4",
            @"5 Date5 Location5"
        };
    }
}