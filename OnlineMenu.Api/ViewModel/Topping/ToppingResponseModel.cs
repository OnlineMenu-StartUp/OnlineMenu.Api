using System.Collections.Generic;
using OnlineMenu.Api.ViewModel.Product;

namespace OnlineMenu.Api.ViewModel.Topping
{
    public class ToppingResponseModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public ICollection<ProductShallowResponseModel> Products { get; set; } = new List<ProductShallowResponseModel>();
    }
}