using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
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
        public Driver_Record Driver_Record { get; set; }
        public Insurer Insurer { get; set; }

        //Many to
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Licence> Licences { get; set; }
        public ICollection<Wanted_Citizen> Wanted_Citizen { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }



    }
}
