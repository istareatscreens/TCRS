using TCRS.Shared.Enums;

namespace TCRS.Shared.Objects.Citations
{
    public class CitationIssueData
    {
        public int citation_type_id { get; set; } = (int)CitationTypes.ParkingCitation; //Test Value
#nullable enable
        public string? licence_id { get; set; }
        public string? licencePlate { get; set; }

        public CitationTypes CitationType { get; set; } = CitationTypes.ParkingCitation;

    }
}
