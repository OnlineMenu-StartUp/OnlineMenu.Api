using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class CategoryController: AppBaseController
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string categoryName)
        {
            await categoryService.CreateCategory(categoryName);
            return Created("", categoryName);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Read()
        {
            return Ok(categoryService.GetAllCategories());
        }
        
        [HttpPut]
        public IActionResult Update([FromBody] Category category)
        {
            categoryService.UpdateCategory(category);
            return Ok(category);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromBody] Category category)
        {
            categoryService.DeleteCategory(category);
            return Ok();
        }
    }
}