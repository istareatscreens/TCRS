using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Wanted
    {
        [Key]
        public int wanted_id { get; set; }
        public string reference_no { get; set; }
        public bool dangerous { get; set; }
        public int crime { get; set; }

        //Relationships
        //One to

        //Many to
        public ICollection<Wanted_Citizen> Wanted_Citizens { get; set; }
        public ICollection<Wanted_Vehicle> Wanted_Vehicles { get; set; }
    }
}
