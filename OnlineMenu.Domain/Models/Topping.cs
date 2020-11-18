using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMenu.Domain.Models
{
    public class Topping
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        
        public ICollection<ProductTopping>? ProductLink { get; set; }
    }
}