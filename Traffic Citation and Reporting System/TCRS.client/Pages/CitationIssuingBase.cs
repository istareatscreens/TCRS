using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Shared.Contracts.CitationManagement;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Client.Pages
{
    public class CitationIssuingBase : ComponentBase
    {

        protected CitationIssueData CitationData { get; set; } = new CitationIssueData();

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(CitationData);
        }

        bool success;

        [Inject]
        private ICitationManager CitationManager { get; set; }

        protected void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            CitationManager.IssueCitation(CitationData);

            success = true;
            StateHasChanged();
        }

        protected string Disabled { get; set; }
        protected string GetOffenderName { get; set; } = "Offender Name Here";
        protected string GetOffenderLicencePlate { get; set; } = "Offender Licence Plate Here";
        protected string GetOfficerName { get; set; } = "Officer Name Here";
        protected string GetDateOfOffence { get; set; } = "Date Of Offence Here";

        protected string CitationNumber { get; set; } = "";
        protected string Status { get; set; } = "";
        protected string DateIssued { get; set; } = "";
        protected string DateDue { get; set; } = "";
        protected string FineAmount { get; set; } = "";
    }
}
