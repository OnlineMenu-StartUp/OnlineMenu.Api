using System;
using System.Collections.Generic;
using System.Linq;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application
{
    public class OrderService
    {
        private readonly IOnlineMenuContext _context;

        public OrderService(IOnlineMenuContext context)
        {
            _context = context;
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.First(o => o.Id == id);
        }
    }
}
