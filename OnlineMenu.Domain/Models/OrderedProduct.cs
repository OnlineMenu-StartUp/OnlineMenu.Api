namespace OnlineMenu.Domain.Models
{
    public class OrderedProduct
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
