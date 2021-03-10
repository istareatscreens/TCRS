
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
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
