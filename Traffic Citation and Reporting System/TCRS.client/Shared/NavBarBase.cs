using Microsoft.AspNetCore.Components;
using TCRS.Client.AuthStateProvider;
using TCRS.Shared.Contracts;

namespace TCRS.Client.Shared
{
    public class NavBarBase : ComponentBase
    {
        [Inject]
        private IUserService User { get; set; }

        [Inject]
        private IAuthServiceProvider authenticationStateProvider { get; set; }
        protected void SignOut()
        {
            //Console.WriteLine((User.User == null)?"":User.User.email);
            authenticationStateProvider.UnsetUser();
        }
    }
}
