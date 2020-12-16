using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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
                (await productService.GetAllProductsAsync()).Select(p => mapper.Map<Product, ProductResponseModel>(p)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetProduct([FromRoute] int id)
        {
            return Ok(mapper.Map<ProductResponseModel>(await productService.GetProductByIdAsync(id)));
        }

        [HttpGet("by-category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory([FromRoute] string categoryName)
        {
            return Ok((await productService.GetProductsByCategoryNameAsync(categoryName)).
                Select(p => mapper.Map<Product, ProductResponseModel>(p)));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateModel productCreate)
        {
            var product = await productService.CreateProductAsync(mapper.Map<Product>(productCreate));
            return Created($"{Request.GetDisplayUrl()}/{product.Id}", mapper.Map<ProductResponseModel>(product));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateModel productUpdate)
        {
            var updatedProduct = await productService.UpdateProductAsync(id, mapper.Map<Product>(productUpdate));
            var productFromDb = await productService.GetProductByIdAsync(updatedProduct.Id); // TODO: To optimize
            return Ok(mapper.Map<ProductResponseModel>(productFromDb));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await productService.DeleteProductAsync(id);
            return Ok(mapper.Map<ProductResponseModel>(product));
        }
    }
}