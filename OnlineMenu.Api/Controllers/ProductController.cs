using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Api.ViewModel.Product;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    public class ProductController: AppBaseController
    {
        private readonly ProductService productService;
        private readonly IMapper mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseModel>>> GetMenu()
        {
            return Ok(
                (await productService.GetAllProducts()).Select(p => mapper.Map<Product, ProductResponseModel>(p)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetProduct([FromRoute] int id)
        {
            return Ok(mapper.Map<ProductResponseModel>(await productService.GetProductById(id)));
        }

        [HttpGet("by-category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory([FromRoute] string categoryName)
        {
            return Ok(await productService.GetProductsByCategoryName(categoryName));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestModel productRequest)
        {
            await productService.CreateProduct(mapper.Map<Product>(productRequest));
            return Created("", productRequest);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateModel product)
        {
            await productService.UpdateProduct(id, mapper.Map<Product>(product));
            return Ok();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await productService.DeleteProduct(id);
            return Ok();
        }
    }
}