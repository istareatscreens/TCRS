using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Registration
    {
        [Key]
        public int citizen_id { get; set; }
        [Key]
        public int course_id { get; set; }
        [Key]
        public int citation_id { get; set; }
        public bool passed { get; set; }

        //Relationship 

        //One to
        public Citation Citaiton { get; set; }
        public Course Course { get; set; }
        public Citizen Citizen { get; set; }

    }
}
