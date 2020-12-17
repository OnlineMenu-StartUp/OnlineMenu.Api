namespace OnlineMenu.Domain.Models
{
    public class ProductTopping
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int ToppingId { get; set; }
        public Topping Topping { get; set; } = null!;
    }
}
