using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Vehicle_Record
    {
        [Key]
        public int vehicle_id { get; set; }

        [Key]
        public int citation_id { get; set; }
    }
}
