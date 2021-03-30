namespace TCRS.Shared.Objects.Citations
{
    public class CitationIssueData
    {

        public int person_id { get; set; }
        public int citation_type_id { get; set; }
#nullable enable
        public string? licence_id { get; set; }
        public string? licencePlate { get; set; }

    }
}
