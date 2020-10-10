using System.Linq;
using OnlineMenu.Domain;

namespace OnlineMenu.Application
{
    public class OrderService
    {
        private readonly IOnlineMenuContext context;

        public OrderService(IOnlineMenuContext context)
        {
            this.context = context;
        }

        public Order GetOrder(int id)
        {
            return context.Orders.First(o => o.Id == id);
        }
    }
}
