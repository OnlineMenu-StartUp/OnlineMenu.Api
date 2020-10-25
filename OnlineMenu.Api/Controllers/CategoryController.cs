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
        public async Task<IActionResult> Read()
        {
            return Ok(await categoryService.GetAllCategories());
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            await categoryService.UpdateCategory(category);
            return Ok(category);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Category category)
        {
            await categoryService.DeleteCategory(category);
            return Ok();
        }
    }
}