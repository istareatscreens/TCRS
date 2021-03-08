using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Wanted_Citizen
    {
        [Key]
        public int wanted_id { get; set; }
        [Key]
        public int citizen_id { get; set; }


        //Relationships
        //One to
        public Citizen Citizen { get; set; }
        public Wanted Wanted { get; set; }
        //Many to


    }
}
