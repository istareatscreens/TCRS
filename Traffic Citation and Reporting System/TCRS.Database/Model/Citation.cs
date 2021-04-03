using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class Citation
    {
        [Key]
        public int citation_id { get; set; }
        public string citation_number { get; set; }
        public DateTime date_recieved { get; set; }
        public int citation_type_id { get; set; }
        public int officer_id { get; set; }
        public bool is_resolved { get; set; }

        //Relationships
        //One to
#nullable enable
        public Citation_Type? Citation_Type { get; set; }

        public Payment? Payment { get; set; }
        public Driver_Record? Driver_Record { get; set; }
        public Vehicle_Record? Vehicle_Record { get; set; }
        public Registration? Registration { get; set; }

        //Many to
        public IEnumerable<Registration>? Registrations { get; set; }


    }
}
