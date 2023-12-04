namespace Assignment5.Models
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Expiration {  get; set; }
        public string CVV { get; set; }
    }
}
