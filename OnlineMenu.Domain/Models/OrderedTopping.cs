using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Domain.Models
{
    public class OrderedTopping
    {
        public int Id { get; set; }

        [Required]
        public Topping Topping { get; set; } = null!;
        
        [Required]
        public int ToppingId { get; set; }

        [Required]
        public OrderedProduct OrderedProduct { get; set; } = null!;
        
        [Range(1, 99)]
        public int Count { get; set; }
    }
}
