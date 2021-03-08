
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Driver_Record
    {
        [Key]
        public int citizen_id { get; set; }

        [Key]
        public int citation_id { get; set; }

        //Relationship
        //Many to
        //One to
        public Citizen Citizen { get; set; }
        public Citation Citation { get; set; }

    }
}

