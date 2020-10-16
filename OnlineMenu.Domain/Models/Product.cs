using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMenu.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        
        public Category Category { get; set; }
        public ICollection<ProductProductExtra> ProductProductExtras { get; set; }
    }
}
