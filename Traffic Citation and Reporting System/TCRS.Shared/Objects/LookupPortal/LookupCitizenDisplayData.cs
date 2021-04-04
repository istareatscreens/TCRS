using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.LookupPortal;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupCitizenDisplayData
    {
        // citizen
        public string first_name { get; set; } = "";
        public string middle_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string GetOffenderName()
        {
            return first_name + " " + middle_name + " " + last_name;
        }

        // licence
        public string license_id { get; set; }
        public DateTime expiration_date { get; set; }
        public bool is_revoked { get; set; }
        public bool is_suspended { get; set; }
        public string license_class { get; set; }

        public IEnumerable<CitizenWantedData> Wanted_Citizen { get; set; }
    }
}
