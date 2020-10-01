using System;
namespace OnlineMenu.Domain.Order
{
    public class OrderedItems
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int Count { get; set; }
    }
}
