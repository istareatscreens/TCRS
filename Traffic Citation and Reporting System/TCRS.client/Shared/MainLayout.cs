using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TCRS.Client.AuthStateProvider;
using TCRS.Client.BusyOverlay;
using TCRS.Shared.Contracts;

namespace TCRS.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {


        [Inject]
        protected IUserService UserService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        private IAuthServiceProvider _authenticationStateProvider { get; set; }
        [Inject]
        protected BusyOverlayService BusyOverlayService { get; set; }

        protected bool IsCurrentRoute(string uri)
        {
            return NavigationManager.Uri == NavigationManager.BaseUri+uri;
        }

        protected async Task SignOut()
        {
            UserService.User = null;
            NavigationManager.NavigateTo("/");
            await _authenticationStateProvider.UnsetUser();

        }
    }
}
