using System.Collections.Generic;
using OnlineMenu.Api.ViewModel.ProductExtra;

namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductRequestModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        
        public int? CategoryId { get; set; }
        
        public ICollection<ToppingShallowRequestModel> Toppings { get; set; } = new List<ToppingShallowRequestModel>();
    }
}