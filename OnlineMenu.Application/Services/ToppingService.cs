using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class ToppingService
    {
        private readonly IOnlineMenuContext context;

        public ToppingService(IOnlineMenuContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Topping>> GetAllToppingsAsync()
        {
            return await context.Toppings
                .Include(t => t.ProductLink)
                .ThenInclude(pt => pt.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }
        
        public async Task<Topping> GetToppingById(int id)
        {
            try
            {
                return await context.Toppings
                    .Include(t => t.ProductLink)
                    .ThenInclude(pt => pt.Product)
                    .ThenInclude(p => p.Category)
                    .FirstAsync(p => p.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException($"Product with id = {id} was not found");
            }
        }

        public async Task<IEnumerable<Topping>> GetToppingsByProductIdAsync(int productId)
        {
            if (!await context.Products.AnyAsync(p => p.Id == productId))
                throw new ValueNotFoundException($"Product with id = {productId} was not found");
            
            return await context.Toppings
                .Include(t => t.ProductLink)
                .ThenInclude(pt => pt.Product)
                .ThenInclude(p => p.Category)
                .Where(t => (t.ProductLink ?? new List<ProductTopping>()).Any(pl => pl.ProductId == productId))
                .ToListAsync();
        }

        public async Task<Topping> CreateTopping(Topping topping)
        {
            // ReSharper disable once MethodHasAsyncOverload
            context.Toppings.Add(topping);
            await context.SaveChangesAsync();
            return topping;
        }

        public async Task<Topping> UpdateToppingAsync(int id, Topping topping)
        {
            if(!await context.Toppings.AnyAsync(t => t.Id == id))
                throw new ValueNotFoundException($"Topping with id = {id} was not found");
            
            topping.Id = id;
            context.Toppings.Update(topping); // TODO: it can only add related entities, but not delete them
            await context.SaveChangesAsync();

            return topping; // TODO: it doesn't return related entities
        }

        public async Task<Topping> DeleteTopping(int id)
        {
            var topping = await context.Toppings.FindAsync(id);
            if (topping == null)
                throw new ValueNotFoundException($"Topping with id = {id} was not found");
            
            context.Toppings.Remove(topping);
            await context.SaveChangesAsync();
            return topping;
        }
    }
}