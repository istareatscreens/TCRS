using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Client_Admin
    {
        [Key]
        public int person_id { get; set; }

        //One to Relationship
        public Person Person { get; set; }

    }
}
