using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Wanted_Vehicle
    {
        [Key]
        public int vehicle_id { get; set; }
        [Key]
        public string wanted_id { get; set; }

        //Relationships
        //One to
        public Vehicle Vehicle { get; set; }
        public Wanted Wanted { get; set; }
        //Many to
    }
}
