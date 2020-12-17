using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineMenu.Api.ViewModel.OrderedProduct;
using OnlineMenu.Api.ViewModel.Product;
using OnlineMenu.Api.ViewModel.Topping;

namespace OnlineMenu.Api.ViewModel.Order
{
    public class OrderRequestModel
    {
        public string? PaymentType { get; set; }
        
        [Required]
        public ICollection<OrderedProductRequestModel> OrderedProducts { get; set; } = new List<OrderedProductRequestModel>();
    }
}