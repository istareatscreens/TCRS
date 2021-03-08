using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Course
    {
        [Key]
        public int course_id { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public DateTime scheduled { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string instructor { get; set; }
        public int capacity { get; set; }
        public int citation_type_id { get; set; }
        public int school_id { get; set; }

        //Relationships
        //one to
        public School School { get; set; }
        public Citation_Type Citation_Type { get; set; }

        //many to
        public ICollection<Registration> Registrations { get; set; }




    }
}
