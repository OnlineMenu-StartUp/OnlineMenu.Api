using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class ProductService
    {
        private readonly IOnlineMenuContext context;

        public ProductService(IOnlineMenuContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.ToppingLinks)
                .ThenInclude(pt => pt.Topping)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ToppingLinks)
                    .ThenInclude(pt => pt.Topping)
                    .FirstAsync(p => p.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException($"Product with id = {id} was not found");
            }
        }
        
        public async Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            try
            {
                return await context.Products.Where(p =>
                        p.Category != null &&
                        string.Equals(p.Category.Name, categoryName, StringComparison.InvariantCultureIgnoreCase))
                    .Include(p => p.Category)
                    .Include(p => p.ToppingLinks)
                    .ThenInclude(pe => pe.Topping)
                    .ToListAsync();
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException($"Product with category name = {categoryName} was not found");
            }
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            // ReSharper disable once MethodHasAsyncOverload
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            if (product.ToppingLinks != null)
                foreach (var toppingLink in product.ToppingLinks)
                    if (!await context.Toppings.AnyAsync(t => t.Id == toppingLink.ToppingId))
                        throw new ValueNotFoundException($"Topping with id = {id} was not found");
            
            if (!await context.Categories.AnyAsync(c => c.Id == product.CategoryId))
                throw new ValueNotFoundException($"Category with id = {id} was not found");
            if (!await context.Products.AnyAsync(p => p.Id == id))
                throw new ValueNotFoundException($"Product with id = {id} was not found");
            
            product.Id = id;
            context.Products.Update(product); // TODO: it can only add related entities, but not delete them
            await context.SaveChangesAsync();
            return product; // TODO: it doesn't return related entities
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await context.Products.FindAsync(productId);
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return product;
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException($"Product with id = {productId} was not found");
            }
        }
    }
}