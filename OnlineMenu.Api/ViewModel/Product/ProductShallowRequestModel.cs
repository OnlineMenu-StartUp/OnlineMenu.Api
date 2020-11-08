namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductShallowRequestModel
    {
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string Category { get; set; } = null!;
    }
}