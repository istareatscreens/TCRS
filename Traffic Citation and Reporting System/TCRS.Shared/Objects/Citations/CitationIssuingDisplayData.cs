using System;

namespace TCRS.Shared.Objects.Citations
{
    public class CitationIssuingDisplayData
    {

        public bool is_citizen { get; set; }
        public bool is_vehicle { get; set; }
        // citizen table
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime dob { get; set; }
        public string home_address { get; set; }
        public string license_id { get; set; }

        public bool is_suspended { get; set; }
        public bool is_revoked { get; set; }
        public string GetOffenderName()
        {
            return first_name + " " + middle_name + " " + last_name;
        }

        // licence_plate
        public string plate_number { get; set; }

        public string vin { get; set; }
        public int year_made { get; set; }
        public string model { get; set; }
        public string make { get; set; }
        public bool stolen { get; set; }

        // citation
        public string citation_number { get; set; }
        public string Status { get; set; }
        public DateTime? date_recieved { get; set; } = null;

        //insurer
        public string insurer { get; set; }

        // citation_type
        public string fine { get; set; }

        public DateTime? DateDue { get; set; } = null;
    }
}
