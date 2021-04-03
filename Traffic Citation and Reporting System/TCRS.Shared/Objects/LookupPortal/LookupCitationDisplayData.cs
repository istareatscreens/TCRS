using System;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupCitationDisplayData
    {
        // citation
        public string citation_number { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string model { get; set; }
        public string name { get; set; }
        public string citation_type_id { get; set; }

        public double fine { get; set; }
        public DateTime date_due { get; set; }
        public string GetOffenderName()
        {
            return first_name + " " + middle_name + " " + last_name;
        }
        //-licence_plate
        public string plate_number { get; set; }
        public string license { get; set; }
        public string license_class { get; set; }
        public bool is_resolved { get; set; }
        public int officer_id { get; set; }
        public DateTime date_recieved { get; set; }
    }
}
