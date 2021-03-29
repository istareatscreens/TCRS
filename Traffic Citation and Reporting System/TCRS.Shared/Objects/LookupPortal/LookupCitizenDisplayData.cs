using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.Lookup
{
    class LookupCitizenDisplayData
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
        public string licence_id { get; set; }
        public DateTime expiration_date { get; set; }
        public int is_revoked { get; set; }
        public int is_suspended { get; set; }
        public string licence_class { get; set; }
    }
}
