using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Client_Admin
    {
        [Key]
        public int person_id { get; set; }

        //One to Relationship
        public Person Person { get; set; }

    }
}
