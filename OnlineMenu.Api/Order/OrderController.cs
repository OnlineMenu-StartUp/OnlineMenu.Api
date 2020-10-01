using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMenu.Application.Order;
using OnlineMenu.Application.Order.Dto;

namespace OnlineMenu.Api.Order
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get the order with the correspondding Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns></return>
        [Route("{orderId}")]
        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
        {
            var order = (await _mediator.Send(new GetOrderByIdQuerry(orderId))) as OrderDto;
            return order;
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="order">Order</param>
        [HttpPost]
        private async Task<ActionResult<OrderDto>> PostOrder([FromBody] OrderDto order)
        {
            var isSuccess = (bool)  await _mediator.Send(new MakeOrderCommand(order));

            return isSuccess ?
                 Created(string.Empty, null) : // TODO: Add middleware error handling
                 (ActionResult<OrderDto>) StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
