using System.Collections.Generic;

namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductUpdateModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        
        public int? CategoryId { get; set; }
        
        public ICollection<int> ToppingsId { get; set; } = new List<int>();
        
    }
}