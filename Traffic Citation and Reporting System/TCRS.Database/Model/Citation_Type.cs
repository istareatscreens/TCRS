using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Citation_Type
    {
        [Key]
        public int citation_type_id { get; set; }
        public string name { get; set; }
        public string fine { get; set; }
        public bool training_eligable { get; set; }
        public int due_date_month { get; set; }

        //Relationships
        //Many to one
        public ICollection<Course> Courses { get; set; }
        public ICollection<Citation> Citations { get; set; }

    }
}
