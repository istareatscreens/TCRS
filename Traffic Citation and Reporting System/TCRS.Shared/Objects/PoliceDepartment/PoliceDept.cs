using System;
using System.Collections.Generic;
using System.Text;

namespace TCRS.Shared.Objects.PoliceDepartment
{
   public class PoliceDept
    {
        public int police_dept_id { get; set; }
        public int manager_id { get; set; }
        public string name { get; set; }
        public int person_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
    }
}
