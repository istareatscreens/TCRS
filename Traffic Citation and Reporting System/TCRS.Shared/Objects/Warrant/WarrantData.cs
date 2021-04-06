namespace TCRS.Shared.Objects.Warrant
{
    public class WarrantData
    {
        public string reference_no { get; set; }
        // only returned
        public bool status { get; set; }
        public string crime { get; set; }
        public bool dangerous { get; set; }
    }
}
