using System;

namespace TCRS.Shared.Objects.LookupPortal
{
    public class CitationData
    {
        public string citation_number { get; set; }
        public string name { get; set; }
        public DateTime date_recieved { get; set; }
        public DateTime date_due { get; set; }
        public double fine { get; set; }
    }
}
