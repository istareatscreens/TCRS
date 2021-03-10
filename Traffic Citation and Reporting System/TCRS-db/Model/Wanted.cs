using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
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
