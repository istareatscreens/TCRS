namespace TCRS.Shared.Objects.Payment
{
    public class PaymentData
    {
        // Post request
        // Parameter object
        public int citation_id { get; set; }
        public string citation_number { get; set; }
        public double payment { get; set; } = 0;
    }
}
