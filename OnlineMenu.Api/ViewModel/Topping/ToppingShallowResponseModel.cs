namespace OnlineMenu.Api.ViewModel.Topping
{
    public class ToppingShallowResponseModel
    {
        public ToppingShallowResponseModel(int id, string name, decimal price) // TODO: add constructors to all response models 
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}