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
        public Employee selectedEmployee { get; set; }
        protected MudDateRangePicker _picker;
        protected DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(5).Date);
        protected List<Employee> EmployeeNames { get; set; } = new List<Employee>();
        protected List<EmployeeLookupData> EmployeeLookupData { get; set; } = new List<EmployeeLookupData>();
        protected EditContext EditContext { get; set; }

        [Inject]
        private IEmployeeLookupManager EmployeeManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var employeeList = await EmployeeManager.GetEmployeeNames();
            this.EmployeeNames = employeeList;
        }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(dateRange);
        }

      
        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            DateTime start_date = (DateTime)dateRange.Start;
            DateTime end_date = (DateTime)dateRange.End;

            var data = await EmployeeManager.GetEmployeeLookup(start_date, end_date);
            this.EmployeeLookupData = data;
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
