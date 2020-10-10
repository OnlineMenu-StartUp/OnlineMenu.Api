using System.Collections;
using System.Collections.Generic;

namespace OnlineMenu.Domain.Models
{
    public class ProductExtra
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductProductExtra> ProductProductExtras { get; set; }
    }
}
