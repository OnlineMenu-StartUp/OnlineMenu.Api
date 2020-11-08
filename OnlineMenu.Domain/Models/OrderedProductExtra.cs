namespace OnlineMenu.Domain.Models
{
    public class OrderedProductExtra
    {
        public int Id { get; set; }
        public Topping Topping { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
