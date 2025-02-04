namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductShallowResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string? CategoryName { get; set; }
    }
}