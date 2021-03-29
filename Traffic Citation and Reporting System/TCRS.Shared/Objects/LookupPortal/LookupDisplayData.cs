using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.Lookup
{
    class LookupDisplayData
    {

        public LookupCitizenDisplayData citizenData { get; set; } = null;
        public LookupCitationDisplayData citationData { get; set; } = null;
        public LookupVehicleDisplayData vehicleData { get; set; } = null;

    }
}
