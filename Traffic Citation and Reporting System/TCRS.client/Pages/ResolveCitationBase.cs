using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.CitationResolution;

namespace TCRS.Client.Pages
{
    public class ResolveCitationBase : ComponentBase
    {
        protected CitationResolutionLoginData LoginData { get; set; } = new CitationResolutionLoginData();

        protected EditContext EditContext { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IResolveCitationManager CitationManager { get; set; }

        [Inject]
        private ICitationService CitationService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(LoginData);
        }

        protected async void OnValidSubmit(EditContext context)
        {
            if (!EditContext.Validate())
            {
                // Print out invalid input message
                return;
            }

            CitationService.SetCitizenVehicleCitations(await CitationManager.CitizenLogin(LoginData));
            NavigationManager.NavigateTo($"/Citationresolution/{LoginData.citation_number}");

            //success = true;
            StateHasChanged();
        }


    }
}
