using System;

namespace TCRS.Shared.Objects.Lookup
{
    public class LookupCitationDisplayData
    {
        //Citizen Information
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        //Vehicle Information
        public string plate_number { get; set; }
        public string model { get; set; }
        public bool is_stolen { get; set; }
        //Citation Type Description and Information
        public string citation_number { get; set; }
        public string name { get; set; }
        public string citation_type_id { get; set; }
        public double fine { get; set; }
        public DateTime date_due { get; set; }
        //Oficer information
        public int officer_id { get; set; }
        public string officer_first_name { get; set; }
        public string officer_last_name { get; set; }
        public bool Is_Municipal_Officer { get; set; }
        public bool Is_Police_Officer { get; set; }
        public string dept { get; set; }
        //status
        public string license { get; set; }
        public bool is_revoked { get; set; }
        public bool is_suspended { get; set; }
        public string license_class { get; set; }
        public bool is_resolved { get; set; }
        public DateTime date_recieved { get; set; }
        public string GetOffenderName()
        {
            return first_name + " " + middle_name + " " + last_name;
        }
        public bool is_vehicle { get; set; }
        public bool is_citizen { get; set; }
        //Insurence
        public bool is_insured { get; set; }
        public string insurer { get; set; }

    }
}
