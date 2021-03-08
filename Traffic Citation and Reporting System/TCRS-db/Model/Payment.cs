using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Payment
    {
        [Key]
        public int citation_id { get; set; }
        public double payment { get; set; }
        public DateTime payment_date { get; set; }
        public int made_by { get; set; }
        public string payment_method { get; set; }


        //Relationship  
        //one to
        public Citation Citation { get; set; }

    }
}

