namespace OnlineMenu.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public Status Status { get; set; }
    }
}
