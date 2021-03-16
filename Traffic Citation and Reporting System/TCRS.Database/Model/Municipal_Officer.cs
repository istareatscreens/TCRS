using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Municipal_Officer
    {
        [Key]
        public int person_id { get; set; }
        public string position { get; set; }
        public int munic_id { get; set; }

        //Relationships one to one
        public Municipality Municipality { get; set; }
        public Person Person { get; set; }

        //Relations one to many
        public ICollection<Citation> Citations { get; set; }

    }
}
