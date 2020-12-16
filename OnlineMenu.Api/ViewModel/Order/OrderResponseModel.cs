using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineMenu.Api.ViewModel.OrderedProduct;
using OnlineMenu.Api.ViewModel.Product;
using OnlineMenu.Api.ViewModel.Topping;

namespace OnlineMenu.Api.ViewModel.Order
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        
        public string? PaymentType { get; set; }
        
        [Required]
        public string Status { get; set; } = null!;
        
        [Required]
        public ICollection<OrderedProductResponseModel> OrderedProducts { get; set; } = new List<OrderedProductResponseModel>();
    }
}