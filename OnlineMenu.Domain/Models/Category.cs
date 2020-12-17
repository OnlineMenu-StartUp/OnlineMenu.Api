using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Name { get; set; } = null!;
    }
}
