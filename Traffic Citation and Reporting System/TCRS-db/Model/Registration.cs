using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Registration
    {
        [Key]
        public int citizen_id { get; set; }
        [Key]
        public string course_id { get; set; }
        [Key]
        public string citation_id { get; set; }
        public bool passed { get; set; }

        //Relationship 

        //One to
        public Citation Citaiton { get; set; }
        public Course Course { get; set; }
        public Citizen Citizen { get; set; }

    }
}