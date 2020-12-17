using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineMenu.Api.ViewModel.OrderedTopping;
using OnlineMenu.Api.ViewModel.Product;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.ViewModel.OrderedProduct
{
    public class OrderedProductResponseModel
    {
        public int Id { get; set; }

        public int Count { set; get; }
        
        public ProductShallowResponseModel Product { get; set; } = null!;

        public ICollection<OrderedToppingResponseModel>? OrderedToppings { get; set; }
    }
}