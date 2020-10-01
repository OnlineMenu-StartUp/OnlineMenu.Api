using System;
namespace OnlineMenu.Domain.Order
{
    public class Order
    {
        public int Id { get; set; }

        public int PaymentTypeId { get; set; }

        public int StatusId { get; set; }
    }
}
