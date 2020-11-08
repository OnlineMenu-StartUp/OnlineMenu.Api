using System.Collections.Generic;
using OnlineMenu.Api.ViewModel.ProductExtra;

namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string? Category { get; set; }

        public ICollection<ToppingShallowResponseModel> Toppings { get; set; } = new List<ToppingShallowResponseModel>();
    }
}