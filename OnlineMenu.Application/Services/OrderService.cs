using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class OrderService
    {
        private readonly IOnlineMenuContext context;

        public OrderService(IOnlineMenuContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await context.Orders
                .Include(o => o.Status)
                .Include(o => o.OrderedProducts)
                .ThenInclude(op => op.Product)
                .Include(o => o.OrderedProducts)
                .ThenInclude(op => op.OrderedToppings)
                .ThenInclude(ot => ot.Topping)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                return await context.Orders
                    .Include(o => o.Status)
                    .Include(o => o.OrderedProducts)
                    .ThenInclude(op => op.Product)
                    .Include(o => o.OrderedProducts)
                    .ThenInclude(op => op.OrderedToppings)
                    .ThenInclude(ot => ot.Topping)
                    .SingleAsync(o => o.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException($"Order with Id = {id} was not found");
            }
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(int id, Order order)
        {
            if (await context.Statuses.AnyAsync(s => s.Id == order.StatusId)) 
                throw new ValueNotFoundException($"Status with id = {id} was not found");

            order.Id = id;
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }
        
        public async Task<Order> DeleteOrderAsync(int id)
        {
            try
            {
                var order = await context.Orders.FindAsync(id);
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                return order;
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException($"Order with id = {id} was not found");
            }
        }
    }
}
