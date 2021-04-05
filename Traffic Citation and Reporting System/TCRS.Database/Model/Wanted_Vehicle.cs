using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Wanted_Vehicle
    {
        [Key]
        public int vehicle_id { get; set; }
        [Key]
        public string wanted_id { get; set; }

        //Relationships
        //One to
#nullable enable
        public Vehicle? Vehicle { get; set; }
        public Wanted? Wanted { get; set; }
        //Many to
    }
}
