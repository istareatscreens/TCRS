﻿using System.Collections.Generic;

namespace TCRS.Shared.Objects.EmployeeLookup
{
    public class EmployeeLookupData : Employee
    {
        // get object
        // person id is passed as parameter
        public int person_id { get; set; }

        // return that person's name, email, etc
        public string email { get; set; }
        public bool active { get; set; }
        public int police_dept_id { get; set; }
        public int munic_id { get; set; }


        public IEnumerable<KeyValuePair<int, int>> CitationCountbyType { get; set; }


    }
}
