using Microsoft.AspNetCore.Mvc;

namespace OnlineMenu.Api.Controllers
{
    /// <summary>
    /// Just not to repeat same attributes in every controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppBaseController: ControllerBase
    { }
}