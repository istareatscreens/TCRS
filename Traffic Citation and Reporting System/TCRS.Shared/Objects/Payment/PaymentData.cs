using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.Payment
{
    public class PaymentData
    {
        // Post request
        // Parameter object
        public int citation_id { get; set; }
        public double payment { get; set; } = 0;
    }
}
