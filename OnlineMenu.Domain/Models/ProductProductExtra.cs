namespace OnlineMenu.Domain.Models
{
    public class ProductProductExtra
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductExtraId { get; set; }
        public ProductExtra ProductExtra { get; set; }
    }
}
