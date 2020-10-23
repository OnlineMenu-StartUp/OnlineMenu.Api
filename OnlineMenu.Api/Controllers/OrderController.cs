using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OnlineMenu.Application.Services;

namespace OnlineMenu.Api.Controllers
{
    public class OrderController : AppBaseController
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult> Get(int orderId)
        {
            return Ok(await orderService.GetOrder(orderId));
        }
    }
}
