﻿namespace TCRS.Shared.Objects.Citations
{
    public class CitationIssuingDisplayData
    {
        public string GetOffenderName { get; set; } = "Offender Name Here";
        public string GetOffenderLicencePlate { get; set; } = "Offender Licence Plate Here";
        public string GetOfficerName { get; set; } = "Officer Name Here";
        public string GetDateOfOffence { get; set; } = "Date Of Offence Here";

        public string citation_number { get; set; } = "";
        public string Status { get; set; } = "";
        public string date_received { get; set; } = "";
        public string DateDue { get; set; } = "";
        public string FineAmount { get; set; } = "";
    }
}
