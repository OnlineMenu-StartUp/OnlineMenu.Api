using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineMenu.Api.ViewModel.OrderedTopping;

namespace OnlineMenu.Api.ViewModel.OrderedProduct
{
    public class OrderedProductRequestModel
    {
        [Range(1, 99)] 
        public int Count { get; set; } = 1;

        [Required]
        public int ProductId { get; set; }
    
        public ICollection<OrderedToppingRequestModel>? OrderedToppings { get; set; }
    }
}