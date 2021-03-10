using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Vehicle
    {
        [Key]
        public int vehicle_id { get; set; }
        public string vin { get; set; }
        public string name { get; set; }
        public bool stolen { get; set; }

        public string make { get; set; }
        public bool registered { get; set; }
        public string model { get; set; }
        public short year_made { get; set; }

        public int? citizen_id { get; set; }
        public int? insurer_id { get; set; }

        //Relationships
        //One to
        public Citizen Citizen { get; set; }
        public Insurer Insurer { get; set; }
        public Vehicle_Record Vehicle_Record { get; set; }

        //Many to
        public ICollection<Wanted_Vehicle> Wanted_Vehicles { get; set; }
        public ICollection<Licence_Plate> Licence_Plates { get; set; }

    }
}
