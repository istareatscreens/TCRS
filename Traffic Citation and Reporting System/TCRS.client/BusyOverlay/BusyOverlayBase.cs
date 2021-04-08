using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCRS.Client.BusyOverlay
{
    public class BusyOverlayBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public BusyOverlayService BusyOverlayService { get; set; }

        protected bool IsBusy { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            BusyOverlayService.BusyStateChanged += HandleBusyStateChanged;
            IsBusy = BusyOverlayService.CurrentBusyState == BusyEnum.Busy;
        }

        private void HandleBusyStateChanged(object sender, BusyChangedEventArgs e)
        {
            IsBusy = e.BusyState == BusyEnum.Busy;
            StateHasChanged();
        }
    }
}
