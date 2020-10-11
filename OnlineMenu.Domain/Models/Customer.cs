namespace OnlineMenu.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public Order Order { get; set; }
    }
}