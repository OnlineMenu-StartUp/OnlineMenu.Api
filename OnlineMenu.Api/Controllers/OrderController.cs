﻿using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;

namespace OnlineMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
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
