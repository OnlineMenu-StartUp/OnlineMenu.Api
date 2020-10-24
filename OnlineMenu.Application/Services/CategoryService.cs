using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            await context.Categories.AddAsync(new Category {Name = categoryName});
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return context.Categories;
        }

        public void UpdateCategory(Category category)
        {
            ValidateName(category.Name);
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
        
        private static void ValidateName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName) && categoryName!.Length > 30)
                throw new ArgumentException("Shouldn't be null or empty", nameof(categoryName));
        }
    }
}