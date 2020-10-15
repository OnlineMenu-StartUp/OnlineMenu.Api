﻿using System.Linq;
using System.Threading.Tasks;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Models;

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
