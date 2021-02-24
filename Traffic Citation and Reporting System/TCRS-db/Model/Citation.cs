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

    }
}
