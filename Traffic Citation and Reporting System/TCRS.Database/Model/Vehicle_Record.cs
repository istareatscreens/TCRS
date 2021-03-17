using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Vehicle_Record
    {
        [Key]
        public int vehicle_id { get; set; }

        [Key]
        public int citation_id { get; set; }

        //Relationships
        //Many to
        //One to
        public Vehicle Vehicle { get; set; }
        public Citation Citation { get; set; }
    }
}
