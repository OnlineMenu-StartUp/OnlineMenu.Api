using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Domain.Models
{
    public class OrderedProduct
    {
        public int Id { get; set; }
     
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public Order Order { get; set; } = null!;
        
        [Required]
        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; } = null!;
        
        public ICollection<OrderedTopping>? OrderedToppings { get; set; }
        
        [Range(1, 99)]
        public int Count { get; set; }
    }
}
