
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Highway_Patrol_Officer
    {
        [Key]
        public int person_id { get; set; }
        public string position { get; set; }
        public int police_debt_id { get; set; }

        //One to relationship

        public Person Person { get; set; }
        public Police_Debt Police_Debt { get; set; }

        //One to Many
        public ICollection<Citation> Citations { get; set; }

    }
}
