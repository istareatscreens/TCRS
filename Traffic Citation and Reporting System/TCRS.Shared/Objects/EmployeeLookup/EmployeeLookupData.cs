using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.EmployeeLookup
{
    public class EmployeeLookupData
    {
        // get object
        // person id is passed as parameter
        public int person_id { get; set; }

        // return that person's name, email, etc
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
        public int police_dept_id { get; set; }
        public int munic_id { get; set; }


        public string GetEmployeeName()
        {
            return first_name + " " + last_name;
        }

        public IEnumerable<KeyValuePair<int, int>> CitationCountbyType { get; set; }


    }
}
