using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineMenu.Api.ViewModel.ProductExtra;

namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductCreateModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; } = null!;
        
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string? Description { get; set; }
        
        [Required]
        [Range(0, 9999999)]
        public decimal Price { get; set; }
        
        public string? Category { get; set; }
        
        public ICollection<ToppingShallowRequestModel> Toppings { get; set; } = new List<ToppingShallowRequestModel>();
    }
}