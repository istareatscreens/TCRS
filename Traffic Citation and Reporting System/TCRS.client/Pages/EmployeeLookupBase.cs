using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Contracts.EmployeeLookup;
using TCRS.Shared.Objects.EmployeeLookup;

namespace TCRS.Client.Pages
{
    public class EmployeeLookupBase : ComponentBase
    {

        [Inject]
        private IEmployeeLookupManager EmployeeManager { get; set; }

        protected List<EmployeeLookupData> EmployeeData { get; set; } = new List<EmployeeLookupData>();

        protected EditContext EditContext { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var employeeList = await EmployeeManager.GetEmployeeLookup();
            this.EmployeeData = employeeList;
        }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(EmployeeData);
        }

        public EmployeeLookupData selectedEmployee { get; set; }
        protected MudDateRangePicker _picker;
        protected DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(5).Date);

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            //success = true;
            StateHasChanged();
        }
        

        // TEMP DATA
        public enum Type
        {
            Emp1,
            Emp2,
            Emp3,
            Emp4,
            Emp5
        }

        public string GetEmployeeName { get; set; } = "Employee Name Here";
        public string GetEmployeeEmail { get; set; } = "Employee Email Here";
        public string GetEmployeeActive { get; set; } = "Employee Name Here";
        public string GetEmployeeDepartment { get; set; } = "Employee Department Here";
        public string GetEmployeeRole { get; set; } = "Employee Role Here";

        protected string[] headings = {"Citation Type", "Citation Issue Sum"};

        protected string[] rows = {
            @"Type1 Sum1",
            @"Type2 Sum2",
            @"Type3 Sum3",
            @"Type4 Sum4",
            @"Type5 Sum5"
        };
    }
}
