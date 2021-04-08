using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCRS.Client.BusyOverlay
{
    public class BusyChangedEventArgs : EventArgs
    {
        public BusyEnum BusyState { get; set; }
    }
}
