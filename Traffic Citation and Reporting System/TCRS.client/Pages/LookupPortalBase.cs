using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Shared.Contracts.LookupPortal;
using TCRS.Shared.Objects.Lookup;

namespace TCRS.Client.Pages
{
    public class LookupPortalBase : ComponentBase
    {
        protected LookupDisplayData LookupData { get; set; } = new LookupDisplayData();

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(LookupData);
        }

        [Inject]
        private ILookupPortalManager LookupPortalManager { get; set; }

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            if (LookupData.CitizenData != "")
            {
                citizenData = await LookupPortalManager.LookupCitizenData(LookupData.CitizenData);
            }
            else if (LookupData.VehicleData != "")
            {
                vehicleData = await LookupPortalManager.LookupVehicleData(LookupData.VehicleData);
            }
            else if (LookupData.CitationData != "")
            {
                citationData = await LookupPortalManager.LookupCitationData(LookupData.CitationData);
            }

            //success = true;
            StateHasChanged();
        }

        protected bool IssueWarrant { get; set; }

        protected LookupCitationDisplayData citationData = new LookupCitationDisplayData();
        protected LookupCitizenDisplayData citizenData = new LookupCitizenDisplayData();
        protected LookupVehicleDisplayData vehicleData = new LookupVehicleDisplayData();

        public string Disabled { get; set; }

        // TEMP DATA
        protected string[] headings = { "Wanted ID", "Reference Number", "Status", "Crime"};

        protected string[] rows = {
            @"ID1 Reference1 Status1 Crime1",
            @"ID2 Reference2 Status2 Crime2",
            @"ID3 Reference3 Status3 Crime3",
            @"ID4 Reference4 Status4 Crime4",
            @"ID5 Reference5 Status5 Crime5"
        };

    }
}