using Microsoft.AspNetCore.Components;
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

        [Parameter]
        public string citation_number { get; set; }

        protected override async Task OnInitializedAsync()
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

            /*
            foreach (var data in (CitizenVehicleCitation))
            {
                foreach (var prop in ObjectPrinter.PropertiesOfType(data))
                {
                    Console.Write(prop.Key + " " + prop.Value);
                }
            }
            */
        }


        protected string[] headings = { "Citation #", "Status", "Date Issued", "Date Due", "Fine Amount", "Payment", "Schedule Training" };
        // temp data

        protected string[] rows = {
            @"1 Date1 Location1 due1 fine1",
            @"2 Date2 Location2 due2 fine2",
            @"3 Date3 Location3 due3 fine3",
            @"4 Date4 Location4 due4 fine4",
            @"5 Date5 Location5 due5 fine5"
        };
    }
}
