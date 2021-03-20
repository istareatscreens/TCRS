using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TCRS.Client.AuthenticationStateProvider;

namespace TCRS.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        public bool IsAuthenticated { get; set; } = true;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IAuthenticationStateProvider AuthenticationStateProvider { get; set; }
        
        //check for authentication status of user and allow loading of data on authentication
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync(); 
            var authState = await AuthenticationStateTask;
            IsAuthenticated = authState.User.Identity.IsAuthenticated;

            if (!IsAuthenticated)
            {
                return;
            }
            //Load user data needed
        }


        public void SignOut()
        {
            AuthenticationStateProvider.UnsetUser();
        }
    }
}
