using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CitationResolution;

namespace TCRS.Client.Pages
{
    public class CitationResolutionBase : ComponentBase
    {
        [Parameter]
        public string citation_id { get; set; }

        protected string[] headings = { "Citation #", "Status", "Date Issued", "Date Due", "Fine Amount", "Payment", "Schedule Training" };
        // temp data

        protected string[] rows = {
            @"1 Date1 Location1 due1 fine1",
            @"2 Date2 Location2 due2 fine2",
            @"3 Date3 Location3 due3 fine3",
            @"4 Date4 Location4 due4 fine4",
            @"5 Date5 Location5 due5 fine5"
        };
    }
}