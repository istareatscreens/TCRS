using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
