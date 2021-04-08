using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.CitationResolution;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Client.Pages
{
    public class CitationResolutionBase : ComponentBase
    {

        protected List<CitizenVehicleCitation> CitizenVehicleCitation { get; set; } = new List<CitizenVehicleCitation>();

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IResolveCitationManager CitationManager { get; set; }
        [Inject]
        private ICitationService CitationService { get; set; }
        [Inject]
        ISnackbar SnackBar { get; set; }

        [Parameter]
        public string citation_number { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                base.OnInitialized();

                //Get citation data
                var data = CitationService.GetCitizenVehicleCitations();
                if (data == null)
                {
                    CitationService.SetCitizenVehicleCitations(await CitationManager.CitizenLogin(new CitationResolutionLoginData { citation_number = citation_number }));
                    data = CitationService.GetCitizenVehicleCitations();
                }

                if (data == null)
                {
                    //navigate away no information to view
                    NavigationManager.NavigateTo("/ResolveCitation");
                }
                else
                {
                    this.CitizenVehicleCitation = data;
                }
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }

        protected string[] headings = { "Citation Number", "Citation Type", "Date Received", "Date Due", "Training Eligable", "Fine (CAD)" };
    }
}
