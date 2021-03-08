using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class School
    {
        [Key]
        public int school_id { get; set; }
        public string name { get; set; }
        public string phone_no { get; set; }

        //one to many relationship
        public ICollection<School_Rep> School_Reps { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
