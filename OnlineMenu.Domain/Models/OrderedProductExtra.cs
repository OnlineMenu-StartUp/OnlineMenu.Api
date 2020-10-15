namespace OnlineMenu.Domain.Models
{
    public class OrderedProductExtra
    {
        public int Id { get; set; }
        public ProductExtra ProductExtra { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
