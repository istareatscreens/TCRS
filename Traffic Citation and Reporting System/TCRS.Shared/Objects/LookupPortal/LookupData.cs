using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupData
    {
#nullable enable
        // get requests
        // return information based on what was not null
        // passed as parameters, not used by backend
        public string? licence_id { get; set; }
        public string? plate_number { get; set; }
        public string? citation_id { get; set; }

    }
}
