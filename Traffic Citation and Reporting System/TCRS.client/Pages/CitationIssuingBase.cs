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

        //bool success;

        // TEMP DATA
        public Type typeValue { get; set; }
        public enum Type
        {
            RunningRed,
            SeatBeltViolation,
            FailureToFollowRightOfWay,
            FailureToSignalWhenChangingLanes,
            Speeding,
            DrivingOverAMedian,
            DrivingIllegally,
            RunningOverAPedestrianLane,
            DrivingPastASchoolBuswhenUnloadingLoading,
            FailureToDriveWithinASpecifiedLane,

            FixItTicket,
            ParkingCitation,
            MovingVehicleCodeWarning,
            MovingVehicleCodeViolation
        }

        [Inject]
        private ICitationManager CitationManager { get; set; }

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            data = await CitationManager.IssueCitation(CitationData);

            //success = true;
            StateHasChanged();
        }

        protected string Disabled { get; set; }
        protected CitationIssuingDisplayData data = new CitationIssuingDisplayData();

    }
}
