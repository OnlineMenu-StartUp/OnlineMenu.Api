using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductUpdateModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string? Description { get; set; }
        
        [Required]
        [Range(0, 9999999)]
        public decimal Price { get; set; }
        
        public int? CategoryId { get; set; }
        
        public ICollection<int>? ToppingIds { get; set; } = new List<int>();
        
    }
}