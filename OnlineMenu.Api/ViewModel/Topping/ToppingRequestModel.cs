using System.Collections.Generic;

namespace OnlineMenu.Api.ViewModel.ProductExtra
{
    public class ToppingRequestModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<int>? ProductIds { get; set; }
    }
}