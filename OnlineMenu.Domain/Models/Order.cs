namespace OnlineMenu.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; } // temporary, until we add products
        public Status Status { get; set; }
    }
}
