namespace TCRS.Shared.Objects.Warrant
{
    public class CreateWarrantObject
    {
        public string license_id { get; set; }
        public string reference_no { get; set; }
        public bool dangerous { get; set; }
        public string crime { get; set; }
    }
}
