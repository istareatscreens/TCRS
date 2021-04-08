using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Contracts.EmployeeLookup;
using TCRS.Shared.Objects.EmployeeLookup;
using MudBlazor;

namespace TCRS.Client.Pages
{
    public class EmployeeLookupBase : ComponentBase
    {
        protected Employee selectedEmployee { get; set; }
        protected EmployeeLookupData displayActiveEmployee { get; set; } = new EmployeeLookupData();
        protected MudDateRangePicker _picker;
        protected DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(5).Date);
        protected List<Employee> EmployeeNames { get; set; } = new List<Employee>();
        protected List<EmployeeLookupData> EmployeeLookupData { get; set; } = new List<EmployeeLookupData>();
        protected EditContext EditContext { get; set; }

        [Inject]
        private IEmployeeLookupManager EmployeeManager { get; set; }

        [Inject]
        ISnackbar SnackBar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                base.OnInitialized();
                var employeeList = await EmployeeManager.GetEmployeeNames();
                this.EmployeeNames = employeeList;
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(dateRange);
        }

        protected async void OnValidSubmit(EditContext context)
        {

            try
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

                // Set the selected employee to be active
                displayActiveEmployee = EmployeeLookupData.Find(item => item.GetEmployeeName() == selectedEmployee.GetEmployeeName());

                //success = true;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }

        protected string[] headings = { "Citation Type", "Citation Issue Sum" };

    }
}
