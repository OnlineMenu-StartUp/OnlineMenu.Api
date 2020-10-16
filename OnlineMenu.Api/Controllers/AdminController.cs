using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;
using OnlineMenu.Application.ViewModel;

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
        
        // [Authorize(Roles.Admin)]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            adminService.Create(registerModel.UserName, registerModel.Password);

            return Created(string.Empty, null);
        }

        [Authorize(Roles.Admin)]
        [HttpPost("registerCook")]
        public IActionResult RegisterCook([FromBody] RegisterModel cookModel)
        {
            cookService.Create(cookModel.UserName, cookModel.Password);

            return Created(string.Empty, null);
        }

        [HttpPost("authenticate")]
        public ActionResult<AuthenticateResponseModel> Authenticate([FromBody] AuthenticateRequestModel model)
        {
            var authToken = adminService.Authenticate(model.UserName, model.Password);
            
            return Ok(new AuthenticateResponseModel(model.UserName, authToken));
        }
    }
}