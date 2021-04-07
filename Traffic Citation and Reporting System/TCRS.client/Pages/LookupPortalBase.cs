using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Contracts.LookupPortal;
using TCRS.Shared.Contracts.Warrant;
using TCRS.Shared.Objects.Citations;
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
        protected List<WarrantData> warrantCitizenData { get; set; } = new List<WarrantData>();
        protected List<WarrantData> warrantVehicleData { get; set; } = new List<WarrantData>();

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

        [Inject]
        ISnackbar SnackBar { get; set; }

        protected async void OnValidSubmit(EditContext context)
        {
            try
            {
                if (!EditContext.Validate())
                {
                    // Print out invalid input message
                    return;
                }

                if (curTab == 1)
                {
                    citizenData = await LookupPortalManager.LookupCitizenData(LookupData.CitizenData);
                    warrantCitizenData = citizenData.CitizenWantedData.ToList();
                }
                else if (curTab == 2)
                {
                    vehicleData = await LookupPortalManager.LookupVehicleData(LookupData.VehicleData);
                    warrantVehicleData = vehicleData.WarrantData.ToList();
                }
                else if (curTab == 3)
                {
                    citationData = await LookupPortalManager.LookupCitationData(LookupData.CitationData);
                }

                //success = true;
                StateHasChanged();
            }
            catch(Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
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
            warrantCitizenData = new List<WarrantData>();
        }
        protected void clearVehicleForm()
        {
            vehicleData = new LookupVehicleDisplayData();
            createWarrantData = new CreateWarrantObject();
            warrantVehicleData = new List<WarrantData>();

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

        public string Disabled { get; set; }

        // Warrant data headings
        protected string[] headings = { "Reference Number", "Crime", "Dangerous" };

        protected async Task postWarrantData()
        {
            try
            {
                // citizen
                if (curTab == 1)
                {
                    createWarrantData.license_id = citizenData.license_id;
                    await WarrantManager.PostWarrant(createWarrantData);
                    warrantCitizenData = await WarrantManager.GetWarrants(createWarrantData);
                    createWarrantData = new CreateWarrantObject();
                }
                // vehicle
                else if (curTab == 2)
                {
                    createWarrantData.plate_number = vehicleData.plate_number;
                    await WarrantManager.PostWarrant(createWarrantData);
                    warrantVehicleData = await WarrantManager.GetWarrants(createWarrantData);
                    createWarrantData = new CreateWarrantObject();
                }

                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }

        }

        protected async Task removeWarrant(string reference_no)
        {
            try
            {
                // citizen
                if (curTab == 1)
                {
                    await WarrantManager.RemoveWarrant(reference_no);
                    warrantCitizenData = await WarrantManager.GetWarrants(new CreateWarrantObject { license_id = citizenData.license_id });
                }
                // vehicle
                else if (curTab == 2)
                {
                    await WarrantManager.RemoveWarrant(reference_no);
                    warrantVehicleData = await WarrantManager.GetWarrants(new CreateWarrantObject { plate_number = vehicleData.plate_number });
                }
                StateHasChanged();
            }
            catch(Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }

        }

        protected async Task ResolveCitation()
        {
            try
            {
                await LookupPortalManager.ResolveCitation(new RemoveCitationObject { citation_number = citationData.citation_number });
                citationData = await LookupPortalManager.LookupCitationData(LookupData.CitationData);
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
        }
    }
}
