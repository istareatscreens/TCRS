﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.EmployeeLookup
{
    class EmployeeLookupData
    {
        // person
        public int person_id { get; set; }
        public string first_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string email { get; set; } = "";
        public int active { get; set; } = 0;
        public int police_dept_id { get; set; } = 0;


        public string GetEmployeeName()
        {
            return first_name + " " + last_name;
        }


    }
}
