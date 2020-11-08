using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Product>> GetAllProducts()
        {
            return await context.Products
                .Include(p => p.ToppingLinks)
                .ThenInclude(pe => pe.Topping)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.Where(p => p.Id == id)
                .Include(p => p.ToppingLinks)
                .ThenInclude(pe => pe.Topping).FirstAsync();
        }
        
        public async Task<List<Product>> GetProductsByCategoryName(string categoryName)
        {
            return await context.Products.Where(p =>
                    p.Category != null &&
                    string.Equals(p.Category.Name, categoryName, StringComparison.InvariantCultureIgnoreCase))
                .Include(p => p.ToppingLinks)
                .ThenInclude(pe => pe.Topping)
                .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            // TODO: product validation

            // ReSharper disable once MethodHasAsyncOverload
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(int id, Product product)
        {
            // TODO: validation
            
            product.Id = id;
            context.Products.Update(product);
            // var productFromDb = await context.Products.FindAsync(id);
            //
            // var productProductExtrasFromDb = context.ProductToppings.Where(ppe => ppe.ProductId == id);
            // context.ProductToppings.RemoveRange(productProductExtrasFromDb);
            // context.Toppings.AddRange(product.ToppingLinks.Select(ppe => ppe.Topping));
            // context.ProductToppings.AddRange(product.ToppingLinks ?? throw new Exception());

            await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}