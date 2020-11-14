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
            catch (InvalidOperationException _)
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
            catch (InvalidOperationException _)
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
            // TODO: validation
            
            product.Id = id;
            context.Products.Update(product);

            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}