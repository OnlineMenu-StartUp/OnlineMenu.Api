using System.Collections.Generic;

namespace OnlineMenu.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public Status Status { get; set; }
        public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
        public ICollection<OrderedProductExtra> OrderedProductExtras { get; set; } = new List<OrderedProductExtra>();
    }
}
