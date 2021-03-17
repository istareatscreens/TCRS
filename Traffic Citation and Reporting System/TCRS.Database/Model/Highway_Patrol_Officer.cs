using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Highway_Patrol_Officer
    {
        [Key]
        public int person_id { get; set; }
        public string position { get; set; }
        public int police_debt_id { get; set; }

        //One to relationship

        public Person Person { get; set; }
        public Police_Dept Police_Dept { get; set; }

        //One to Many
        public ICollection<Citation> Citations { get; set; }

    }
}
