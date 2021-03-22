using System;
using System.Collections.Generic;
using System.Text;

namespace TCRS.Shared.Objects.Citizens
{
    public class CitizenCitation
    {
        public int citizen_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }

        public int citation_id { get; set; }
        public string citation_number { get; set; }
        public string name { get; set; }
        public DateTime date_recieved { get; set; }
        public bool training_eligable { get; set; }

        public int vehicle_id { get; set; }
        public double fine { get; set; }
        public string plate_number { get; set; }

    }
}
