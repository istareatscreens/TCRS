using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Insurer
    {
        [Key]
        public int insurer_id { get; set; }
        public string name { get; set; }

        //Relationships
        //One to

        //Many to
        public ICollection<Citizen> Citizens { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }

    }
}

