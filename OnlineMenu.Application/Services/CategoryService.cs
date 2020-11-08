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

        public async Task CreateCategory(string categoryName)
        {
            ValidateName(categoryName);
            // ReSharper disable once MethodHasAsyncOverload https://stackoverflow.com/questions/42034282/are-there-dbset-updateasync-and-removeasync-in-net-core/42042173
            context.Categories.Add(new Category {Name = categoryName});
            await context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            ValidateName(category.Name);
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        private static void ValidateName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName) && categoryName!.Length > 30)
                throw new BadValueException("Category Name shouldn't be null or empty");
        }
    }
}