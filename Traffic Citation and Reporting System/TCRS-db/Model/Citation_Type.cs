using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Citation_Type
    {
        [Key]
        public int citation_type_id { get; set; }
        public string name { get; set; }
        public string fine { get; set; }
        public bool training_eligable { get; set; }

        //Relationships
        //Many to one
        public ICollection<Course> Courses { get; set; }
        public ICollection<Citation> Citations { get; set; }

    }
}
