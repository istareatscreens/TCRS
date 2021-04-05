using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        //bool success;

        [Inject]
        private ICitationManager CitationManager { get; set; }

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            if(curTab == 1)
            {
                CitationData.citation_type_id = (int)CitationData.citizenCitationTypes;
            }
            else
            {
                CitationData.citation_type_id = (int)CitationData.vehicleCitationTypes;
            }

            data = await CitationManager.IssueCitation(CitationData);

            //Clear data from form
            CitationData = new CitationIssueData();
            //success = true;
            StateHasChanged();
        }

        protected string Disabled { get; set; }
        protected CitationIssuingDisplayData data = new CitationIssuingDisplayData();

        protected void currentTab(int x)
        {
            curTab = x;
        }

        protected string DangerInfo()
        {
            if (IsDangerous())
            {
                return "Danger info";
            }
            return "";
        }
        protected bool IsDangerous()
        {
            return true;
        }

        protected string WarrantInfo()
        {
            if (HasWarrant())
            {
                return "Warrant info";
            }
            return "";
        }
        protected bool HasWarrant()
        {
            return false;
        }

    }
}
