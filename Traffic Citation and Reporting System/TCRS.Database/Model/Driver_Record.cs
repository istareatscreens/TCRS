using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Driver_Record
    {
        [Key]
        public int citizen_id { get; set; }

        [Key]
        public int citation_id { get; set; }

        //Relationship
        //Many to
        //One to
        public Citizen Citizen { get; set; }
        public Citation Citation { get; set; }

    }
}

