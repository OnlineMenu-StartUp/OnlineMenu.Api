using System.ComponentModel.DataAnnotations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OnlineMenu.Domain.Models
{
    public class Admin
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string UserName { get; set; } = null!;

        [Required]
        public byte[] Password { get; set; } = null!;
        
        [Required]
        public byte[] PasswordSalt { get; set; } = null!;
    }
}