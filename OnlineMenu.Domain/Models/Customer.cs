using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        public int TableId { get; set; }
        
        public Order? Order { get; set; }
    }
}