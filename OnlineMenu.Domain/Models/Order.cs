using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public PaymentType? PaymentType { get; set; }
        
        [Required]
        public Status Status { get; set; } = null!;
        
        [Required]
        public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
        
        public ICollection<OrderedProductExtra>? OrderedProductExtras { get; set; } = new List<OrderedProductExtra>();
    }
}
