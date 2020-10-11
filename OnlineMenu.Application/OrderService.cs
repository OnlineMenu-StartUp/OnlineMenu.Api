using System.Threading.Tasks;
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

        public async Task<Order> GetOrder(int id)
        {
            return await context.Orders.FindAsync(id);
        }
    }
}
