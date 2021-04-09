using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TCRS.Client.AuthStateProvider;
using TCRS.Client.BusyOverlay;
using TCRS.Shared.Contracts;

namespace TCRS.Client.Shared
{
    public class NavBarBase : ComponentBase
    {
        [Parameter]
        public RenderFragment NavButtons { get; set; }

        [Parameter]
        public RenderFragment LoginLogout { get; set; }

        [Parameter]
        public string username { get; set; }

    }
}
