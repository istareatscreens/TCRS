using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Municipality
    {
        //same as person_id
        public int manager_id { get; set; }
        [Key]
        public int munic_id { get; set; }
        public string name { get; set; }

        //Many relationship
        public ICollection<Municipal_Officer> Municipal_Officers { get; set; }

        //One to realtionship
        public Person Person { get; set; }

    }
}
