using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Citizen
    {
        [Key]
        public int citizen_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime dob { get; set; }
        public string home_address { get; set; }
        public int insurer_id { get; set; }


        //Relationship 
        //One to
#nullable enable
        public Driver_Record? Driver_Record { get; set; }
        public Insurer? Insurer { get; set; }
        public License? License { get; set; }

#nullable restore
        //Many to
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<License> Licences { get; set; }
        public ICollection<Wanted_Citizen> Wanted_Citizen { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }



    }
}
