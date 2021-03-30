using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS.Shared.Objects.CitationResolution
{
    public class CitationResolutionData
    {
        // citation
        public int citation_id { get; set; }
        public string status { get; set; }
        public DateTime date_recieved { get; set; }
        public DateTime date_due { get; set; }
        // citation_type
        public double fine { get; set; }
        public int training_eligable { get; set; }

    }
}
