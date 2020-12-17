using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Api.ViewModel.OrderedTopping
{
    public class OrderedToppingRequestModel
    {
        [Range(1, 99)]
        public int Count { get; set; } = 1;

        [Required]
        public int ToppingId { get; set; }
    }
}