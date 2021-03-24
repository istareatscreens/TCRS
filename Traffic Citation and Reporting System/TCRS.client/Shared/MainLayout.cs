using Microsoft.AspNetCore.Components;
using TCRS.Client.AuthStateProvider;

namespace TCRS.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IAuthServiceProvider authServiceProvider { get; set; }

        public void SignOut()
        {
            authServiceProvider.UnsetUser();
        }
    }
}
