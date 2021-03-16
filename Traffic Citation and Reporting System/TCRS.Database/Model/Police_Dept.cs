using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Police_Dept
    {
        public int manager_id { get; set; }
        [Key]
        public int police_dept_id { get; set; }
        public string name { get; set; }


        //one to relationship
        public Person Persons { get; set; }

        //one to many relationship
        public ICollection<Highway_Patrol_Officer> Highway_Patrol_Officers { get; set; }

    }
}
