using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Api.ViewModel.Product
{
    public class ProductShallowRequestModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; } = null!;
        
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string? Description { get; set; }
        
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public decimal Price { get; set; }
        
        public string Category { get; set; } = null!;
    }
}