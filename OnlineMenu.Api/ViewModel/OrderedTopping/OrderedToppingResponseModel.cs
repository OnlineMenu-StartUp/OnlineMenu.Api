using OnlineMenu.Api.ViewModel.Topping;

namespace OnlineMenu.Api.ViewModel.OrderedTopping
{
    public class OrderedToppingResponseModel
    {
        public int Id { get; set; }

        public int Count { get; set; }
        
        public ToppingShallowResponseModel Topping { get; set; } = null!;
    }
}