using Microsoft.AspNetCore.Components;
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
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }
        [Inject]
        private IAuthServiceProvider authServiceProvider { get; set; }

        public void SignOut()
        {
            authServiceProvider.UnsetUser();
        }
    }
}
