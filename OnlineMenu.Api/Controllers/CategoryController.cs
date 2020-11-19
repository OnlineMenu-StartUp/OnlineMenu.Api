using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;

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
        public async Task<IActionResult> Create([FromBody] string name)
        {
            var category = await categoryService.CreateCategory(name);
            return Created($"{Request.GetDisplayUrl()}/{category.Id}", category);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Read()
        {
            return Ok(await categoryService.GetAllCategories());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] string categoryName)
        {
            var category = await categoryService.UpdateCategory(id, categoryName);
            return Ok(category);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await categoryService.DeleteCategory(id);
            return Ok(category);
        }
    }
}