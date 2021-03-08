using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Person
    {
        [Key]
        public int person_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Boolean active { get; set; }

        //one to one Relationship 

        //User roles
        public Client_Admin Client_Admin { get; set; }
        public School_Rep School_Rep { get; set; }
        public Highway_Patrol_Officer Highway_Patrol_Officer { get; set; }
        public Municipal_Officer Municipal_Officer { get; set; }

        //Managers
        public Police_Debt Police_Debt { get; set; }
        public Municipality Municipality { get; set; }

    }
}
