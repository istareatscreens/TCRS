using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Wanted_Citizen
    {
        [Key]
        public int wanted_id { get; set; }
        [Key]
        public int citizen_id { get; set; }


        //Relationships
        //One to
        public Citizen Citizen { get; set; }
        public Wanted Wanted { get; set; }
        //Many to


    }
}
