using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
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
            if (UserService.User != null)
            {
                if (UserService.User.isSchool_Rep)
                {
                    NavigationManager.NavigateTo("/Courseposting");
                }
                else
                {
                    NavigationManager.NavigateTo("/Citationissuing");
                }
            }
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

        [Inject]
        ISnackbar SnackBar { get; set; }

        protected async void HandleSubmit()
        {
            try
            {
                if (!EditContext.Validate())
                {
                    return;
                }
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                var tokensAcquired = await UserManager.UserSignIn(UserCredentials);
                if (tokensAcquired != null)
                {
                    authenticationStateProvider.SetAuthenticatedState(tokensAcquired);
                    NavigationManager.NavigateTo(UserService.User.isSchool_Rep ? "/Courseposting" : "/Citationissuing");
                }
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
    }
}
