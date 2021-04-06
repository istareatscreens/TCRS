using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.Warrant
{
    public class WarrantData
    {
        public int wanted_id;
        public string reference_number;
        // only returned
        public string status;
        public string crime;
    }
}
