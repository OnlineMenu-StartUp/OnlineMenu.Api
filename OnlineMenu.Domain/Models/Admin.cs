using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Domain.Models
{
    public class Admin
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        
        [Required]
        public byte[] Password { get; set; }
        
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}