using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Api.ViewModel.ProductExtra
{
    public class ToppingShallowRequestModel
    {
        
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 9999999)]
        public decimal Price { get; set; }
    }
}