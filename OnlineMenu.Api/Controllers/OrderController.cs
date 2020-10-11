using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Get(int orderId)
        {
            return Ok(orderService.GetOrder(orderId));
        }
    }
}
