using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Api.ViewModel.Authentication;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;

namespace OnlineMenu.Api.Controllers
{
    public class AdminController: AppBaseController
    {
        private readonly AdminService adminService;
        private readonly CookService cookService;


        public AdminController(AdminService adminService, CookService cookService)
        {
            this.adminService = adminService;
            this.cookService = cookService;
        }
        
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            await adminService.CreateAsync(registerModel.UserName, registerModel.Password);

            return Created(string.Empty, null);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("registerCook")]
        public async Task<IActionResult> RegisterCook([FromBody] RegisterModel cookModel)
        {
            await cookService.CreateAsync(cookModel.UserName, cookModel.Password);

            return Created(string.Empty, null);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponseModel>> Authenticate([FromBody] AuthenticateRequestModel model)
        {
            var authToken = await adminService.AuthenticateAsync(model.UserName, model.Password);
            
            return Ok(new AuthenticateResponseModel(model.UserName, authToken));
        }
    }
}