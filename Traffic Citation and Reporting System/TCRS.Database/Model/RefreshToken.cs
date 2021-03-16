using System;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public partial class RefreshToken
    {
        [Key]
        public int token_id { get; set; }
        public int person_id { get; set; }
        public string token { get; set; }
        public DateTime expiry_date { get; set; }

        //Relationships
        //One to
        public Person Person { get; set; }



        //Many to

    }
}
