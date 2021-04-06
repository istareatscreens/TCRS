using System.Collections.Generic;
using TCRS.Shared.Objects.LookupPortal;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupVehicleDisplayData
    {
        // vehicle
        public string plate_number { get; set; }
        public string vin { get; set; }
        public string name { get; set; }
        public bool stolen { get; set; }
        public string make { get; set; }
        public bool registered { get; set; }
        public string model { get; set; }
        public int year_made { get; set; }
        public int? citizen_id { get; set; }
        public int? insurer_id { get; set; }
        public string? insurer_name { get; set; }
        public bool is_insured { get; set; }

#nullable enable
        public LookupCitizenDisplayData? Owner { get; set; }
        public IEnumerable<WarrantData?>? WarrantData { get; set; }
        public IEnumerable<CitationData?>? CitationData { get; set; }
#nullable restore
        //    public LookupCitizenDisplayData? lookupCitizenDisplayData { get; set; }
    }
}
