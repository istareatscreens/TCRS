using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Client.AuthStateProvider;
using TCRS.Client.BusyOverlay;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.Pages
{
    public class SigninBase : ComponentBase
    {
        protected UserLoginCredentials UserCredentials { get; set; } = new UserLoginCredentials();

        protected EditContext EditContext { get; set; }

        protected bool success;

        protected string[] errors = { };

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(UserCredentials);
        }

        [Inject]
        private IUserManager UserManager { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private IAuthServiceProvider authenticationStateProvider { get; set; }

        protected async void HandleSubmit()
        {
            if (!EditContext.Validate())
            {
                return;
            }

            //TODO: add warning message
            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                var tokensAcquired = await UserManager.UserSignIn(UserCredentials);
                if (tokensAcquired != null)
                {
                    authenticationStateProvider.SetAuthenticatedState(tokensAcquired);
                    NavigationManager.NavigateTo(UserService.User.isSchool_Rep ? "/Coursemanagement" : "/Citationissuing");
                }
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }
    }
}
