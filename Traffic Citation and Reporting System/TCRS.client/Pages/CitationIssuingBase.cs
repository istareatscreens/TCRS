using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using TCRS.Client.BusyOverlay;
using TCRS.Shared.Contracts.CitationManagement;
using TCRS.Shared.Enums;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Client.Pages
{
    public class CitationIssuingBase : ComponentBase
    {
        private int curTab = 1;
        protected CitationIssueData CitationData { get; set; } = new CitationIssueData();

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(CitationData);
        }

        [Inject]
        public BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private ICitationManager CitationManager { get; set; }

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
                    CitationData.citation_type_id = (int)CitationData.citizenCitationTypes;
                }
                else if (curTab == 2)
                {
                    CitationData.citation_type_id = (int)CitationData.vehicleCitationTypes;
                }
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                data = await CitationManager.IssueCitation(CitationData);
                //Clear data from form
                CitationData = new CitationIssueData();
                //success = true;
                StateHasChanged();
            }
            catch (Exception e)
            {
                SnackBar.Add(e.Message, Severity.Error);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }

        protected string Disabled { get; set; }
        protected CitationIssuingDisplayData data = null;

        protected void currentTab(int x)
        {
            curTab = x;
            data = new CitationIssuingDisplayData();
        }
    }
}
