using Microsoft.AspNetCore.Mvc;

namespace OnlineMenu.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController: ControllerBase
    {
        [HttpGet]
        public IActionResult Authenticate()
        {
            return null; // TODO: Decide how to implement authentication for customers
        }
    }
}