using System;
using System.Collections.Generic;
using TCRS.Shared.Objects.LookupPortal;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupCitizenDisplayData
    {
        // citizen
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime dob { get; set; }
        public string home_address { get; set; }
        public string GetOffenderName()
        {
            return first_name + ((middle_name != "") ? " " + middle_name + " " + last_name : " " + last_name);
        }
        //insurance 
        public string? insurer_name { get; set; }
        public bool is_insured { get; set; }

        // licence
        public string license_id { get; set; }
        public DateTime expiration_date { get; set; }
        public bool is_revoked { get; set; }
        public bool is_suspended { get; set; }
        public string license_class { get; set; }

#nullable enable
        public IEnumerable<WarrantData?>? CitizenWantedData { get; set; }
        public IEnumerable<CitationData?>? CitationData { get; set; }
#nullable restore
    }
}
