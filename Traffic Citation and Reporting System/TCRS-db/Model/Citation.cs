using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public class Citation
    {
        [Key]
        public int citation_id { get; set; }
        public string citation_number { get; set; }
        public DateTime date_recieved { get; set; }
        public int citation_type_id { get; set; }
        public int officer_id { get; set; }

        //Relationships
        //One to
        public Citation_Type Citation_Type { get; set; }
        public Payment Payment { get; set; }
        public Driver_Record Driver_Record { get; set; }
        public Vehicle_Record Vehicle_Record { get; set; }

        //Many to
        public ICollection<Registration> Registrations { get; set; }


    }
}

