namespace OnlineMenu.Domain.Models
{
    public class OrderedProductExtra
    {
        public int Id { get; set; }

        public Topping Topping { get; set; } = null!;

        public Order Order { get; set; } = null!;
        
        public int Count { get; set; }
    }
}
