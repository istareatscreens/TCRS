using System.Collections.Generic;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupVehicleDisplayData
    {
        // vehicle
        public int vehicle_id { get; set; }
        public string vin { get; set; }
        public string name { get; set; }
        public bool stolen { get; set; }
        public string make { get; set; }
        public bool registered { get; set; }
        public string model { get; set; }
        public int year_made { get; set; }
        public int? citizen_id { get; set; }
        public int? insurer_id { get; set; }
        public IEnumerable<WarrantData> WarrantData { get; set; }
        public IEnumerable<LookupCitationDisplayData> CitationData { get; set; }
        //    public LookupCitizenDisplayData? lookupCitizenDisplayData { get; set; }
    }
}
