using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Contracts.LookupPortal;
using TCRS.Shared.Contracts.Warrant;
using TCRS.Shared.Objects.Lookup;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Client.Pages
{
    public class LookupPortalBase : ComponentBase
    {
        private int curTab = 1;
        protected bool IssueWarrant { get; set; }
        protected LookupCitationDisplayData citationData = new LookupCitationDisplayData();
        protected LookupCitizenDisplayData citizenData = new LookupCitizenDisplayData();
        protected LookupVehicleDisplayData vehicleData = new LookupVehicleDisplayData();

        protected LookupDisplayData LookupData { get; set; } = new LookupDisplayData();
        protected List<WarrantData> warrantData { get; set; } = new List<WarrantData>();

        protected CreateWarrantObject createWarrantData { get; set; } = new CreateWarrantObject();

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(LookupData);
        }

        [Inject]
        private ILookupPortalManager LookupPortalManager { get; set; }

        [Inject]
        private IWarrantManager WarrantManager { get; set; }

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            if (curTab == 1)
            {
                citizenData = await LookupPortalManager.LookupCitizenData(LookupData.CitizenData);
                warrantData = citizenData.CitizenWantedData.ToList();
            }
            else if (curTab == 2)
            {
                vehicleData = await LookupPortalManager.LookupVehicleData(LookupData.VehicleData);
                //warrantData = vehicleData.WarrantData.ToList();
            }
            else if (curTab == 3)
            {
                citationData = await LookupPortalManager.LookupCitationData(LookupData.CitationData);
            }

            //success = true;
            StateHasChanged();
        }

        protected void currentTab(int x)
        {
            curTab = x;
        }

        protected void clearCitationForm()
        {
            citationData = new LookupCitationDisplayData();
            createWarrantData = new CreateWarrantObject();
        }
        protected void clearCitizenForm()
        {
            citizenData = new LookupCitizenDisplayData();
            createWarrantData = new CreateWarrantObject();
            warrantData = new List<WarrantData>();
        }
        protected void clearVehicleForm()
        {
            vehicleData = new LookupVehicleDisplayData();
            createWarrantData = new CreateWarrantObject();
        }

        protected string GetCitationType()
        {
            if (citationData.is_citizen)
            {
                return "Citizen Citation";
            }
            else if (citationData.is_vehicle)
            {
                return "Vehicle Citation";
            }
            return "";
        }

        protected void ResolveCitation()
        {

        }

        public string Disabled { get; set; }

        // Warrant data headings
        protected string[] headings = { "Reference Number", "Crime", "Dangerous" };

        protected async Task postWarrantData()
        {
            // citizen
            if (curTab == 1)
            {
                createWarrantData.license_id = citizenData.license_id;
                await WarrantManager.PostWarrant(createWarrantData);
                warrantData = await WarrantManager.GetWarrants(citizenData.license_id);
            }
            // vehicle
            /*
            else if (curTab == 2)
            {
                // licence plate number
                createWarrantData.plate_number = LookupData.VehicleData;
                await WarrantManager.PostWarrant(createWarrantData);
            }
            */
            StateHasChanged();
        }

        protected async Task removeWarrant(string reference_no)
        {
            // citizen
            if(curTab == 1)
            {
                await WarrantManager.RemoveWarrant(reference_no);
                warrantData = await WarrantManager.GetWarrants(citizenData.license_id);
            }
            // vehicle
            /*
            else if(curTab == 2)
            {
                
            }
            */
            StateHasChanged();
        }

        
    }
}
