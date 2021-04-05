namespace TCRS.Shared.Objects.Payment
{
    public class PaymentData
    {
        // Post request
        // Parameter object
        public int citation_id { get; set; }
        public int citation_number { get; set; }
        public double payment { get; set; } = 0;

        public PaymentData(int citation_id, int citation_number, double payment)
        {
            this.citation_id = citation_id;
            this.citation_number = citation_number;
            this.payment = payment;
        }
    }
}
