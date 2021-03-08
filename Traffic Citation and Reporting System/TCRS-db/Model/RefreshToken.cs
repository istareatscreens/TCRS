using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCRS_db.Model
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
