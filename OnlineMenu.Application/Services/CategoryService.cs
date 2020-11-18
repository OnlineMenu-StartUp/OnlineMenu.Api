using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class CategoryService
    {
        private readonly IOnlineMenuContext context;

        public CategoryService(IOnlineMenuContext context)
        {
            this.context = context;
        }

        public async Task<Category> CreateCategory(string categoryName)
        {
            var category = new Category {Name = categoryName};
            // ReSharper disable once MethodHasAsyncOverload https://stackoverflow.com/questions/42034282/are-there-dbset-updateasync-and-removeasync-in-net-core/42042173
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategory(int categoryId, string categoryName)
        {
            var category = new Category{Id= categoryId, Name = categoryName};
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> DeleteCategory(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            
            return category;
        }
    }
}