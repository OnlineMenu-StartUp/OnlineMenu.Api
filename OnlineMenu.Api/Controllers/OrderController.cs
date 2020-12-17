using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using OnlineMenu.Api.ViewModel.Order;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    public class OrderController : AppBaseController
    {
        private readonly OrderService orderService;
        private readonly IMapper mapper;

        public OrderController(OrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        // [Authorize(Roles = Roles.Admin + ", " + Roles.Cook)]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponseModel>> GetById(int orderId)
        {
            return Ok(mapper.Map<OrderResponseModel>(await orderService.GetOrderByIdAsync(orderId)));
        }
        
        [Authorize(Roles = Roles.Admin + ", " + Roles.Cook)]
        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok((await orderService.GetOrdersAsync()).Select(order => mapper.Map<OrderResponseModel>(order)));
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponseModel>> CreateOrder([FromBody] OrderRequestModel order)
        {
            var createdOrder = await orderService.CreateOrderAsync(mapper.Map<Order>(order));
            return Created($"{Request.GetDisplayUrl()}/{createdOrder.Id}", mapper.Map<OrderResponseModel>(createdOrder));
        }
        
        [HttpPost("id")]
        public async Task<ActionResult<OrderResponseModel>> UpdateOrder([FromRoute] int id, [FromBody] OrderRequestModel order)
        {
            var createdOrder = await orderService.UpdateOrderAsync(id, mapper.Map<Order>(order));
            var orderFromDb = await orderService.GetOrderByIdAsync(createdOrder.Id); // TODO: To optimize
            return Ok(mapper.Map<OrderResponseModel>(orderFromDb));
        }
        
        [HttpPost("id")]
        public async Task<ActionResult<OrderResponseModel>> DeleteOrder([FromRoute] int id, [FromBody] OrderRequestModel order)
        {
            var createdOrder = await orderService.DeleteOrderAsync(id);
            return Ok(mapper.Map<OrderResponseModel>(createdOrder));
        }
    }
}
