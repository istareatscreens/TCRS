using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Police_Debt
    {
        [Key]
        public int police_debt_id { get; set; }
        public int manager_id { get; set; }
        public string name { get; set; }


        //one to relationship
        public Person Persons { get; set; }

        //one to many relationship
        public ICollection<Highway_Patrol_Officer> Highway_Patrol_Officers { get; set; }

    }
}
