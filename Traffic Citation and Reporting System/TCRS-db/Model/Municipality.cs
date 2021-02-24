using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Municipality
    {
        [Key]
        //same as person_id
        public int munic_id { get; set; }
        public int manager_id { get; set; }
        public string name { get; set; }

    }
}
