using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.Lookup
{
    class LookupCitationDisplayData
    {
        // citation
        public int citation_id { get; set; }
        public string first_name { get; set; } = "";
        public string middle_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string GetOffenderName()
        {
            return first_name + " " + middle_name + " " + last_name;
        }
        //-licence_plate
        public string plate_number { get; set; }
        public int officer_id { get; set; }
        public DateTime date_recieved { get; set; }
    }
}
