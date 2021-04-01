using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.EmployeeLookup
{
    public class EmployeeCitationsLookup
    {
        public int person_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public string citation_number { get; set; }
        public DateTime date_recieved { get; set; }

        public string name { get; set; }
        public string fine { get; set; }
        public bool training_eligable { get; set; }
        public int due_date_month { get; set; }
    }
}
