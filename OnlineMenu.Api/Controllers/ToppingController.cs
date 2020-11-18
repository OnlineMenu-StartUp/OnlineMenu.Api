using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Api.ViewModel.ProductExtra;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    public class ToppingController: AppBaseController
    {
        private readonly ToppingService toppingService;
        private readonly IMapper mapper;

        public ToppingController(ToppingService toppingService, IMapper mapper)
        {
            this.toppingService = toppingService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingResponseModel>>> GetTopics()
        {
            return Ok((await toppingService.GetAllToppingsAsync()).Select(t => mapper.Map<ToppingResponseModel>(t)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ToppingResponseModel>>> GetTopic([FromRoute] int id)
        {
            return Ok(mapper.Map<ToppingResponseModel>(await toppingService.GetToppingById(id)));
        }
        
        [HttpGet("by-productId/{productId}")]
        public async Task<ActionResult<IEnumerable<ToppingResponseModel>>> GetToppingsForProduct([FromRoute] int productId)
        {
            return Ok((await toppingService.GetToppingsByProductIdAsync(productId)).Select(t => mapper.Map<ToppingResponseModel>(t)));
        }

        [HttpPost]
        public async Task<ActionResult<Topping>> CreateTopping([FromBody] ToppingShallowRequestModel toppingCreate)
        {
            var createdTopping = await toppingService.CreateTopping(mapper.Map<Topping>(toppingCreate));
            return Created("", mapper.Map<ToppingResponseModel>(createdTopping));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Topping>> UpdateTopping([FromRoute] int id, [FromBody] ToppingRequestModel toppingUpdate)
        {
            var updatedTopping = await toppingService.UpdateToppingAsync(id, mapper.Map<Topping>(toppingUpdate));
            return Ok(mapper.Map<ToppingResponseModel>(updatedTopping));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Topping>> DeleteTopping([FromRoute] int id)
        {
            var deletedTopping = await toppingService.DeleteTopping(id);
            return Ok(mapper.Map<ToppingResponseModel>(deletedTopping));
        }
    }
}