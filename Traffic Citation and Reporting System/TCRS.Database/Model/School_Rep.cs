using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class School_Rep
    {
        [Key]
        public int person_id { get; set; }

        [Key]
        public int school_id { get; set; }

        //Relationships one to one
        public Person Person { get; set; }
        public School School { get; set; }

    }
}
