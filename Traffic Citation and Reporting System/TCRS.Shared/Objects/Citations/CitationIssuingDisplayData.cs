using System;

namespace TCRS.Shared.Objects.Citations
{
    public class CitationIssuingDisplayData
    {
        // citizen table
        public string first_name { get; set; } = "";
        public string middle_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string GetOffenderName()
        {
            return first_name + " " + middle_name + " " + last_name;
        }

        // licence_plate
        public string plate_number { get; set; } = "";

        // citation
        public string citation_number { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime? date_received { get; set; } = null;

        // citation_type
        public string fine { get; set; } = "";

        public DateTime? DateDue { get; set; } = null;
    }
}
