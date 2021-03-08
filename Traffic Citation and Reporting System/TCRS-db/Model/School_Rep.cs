using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class School_Rep
    {
        [Key]
        public int person_id { get; set; }

        [Key]
        public int school_id { get; set; }

        //Relationships one to one
        public Person Person { get; set; }
        public School School { get; set; }

    }
}
