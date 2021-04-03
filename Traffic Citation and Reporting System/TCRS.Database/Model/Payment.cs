using System;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Payment
    {
        [Key]
        public int citation_number { get; set; }
        public double payment { get; set; }
        public DateTime payment_date { get; set; }
        public string made_by { get; set; }
        public string payment_method { get; set; }


        //Relationship  
        //one to
        public Citation Citation { get; set; }

    }
}
