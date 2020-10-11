using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;
using OnlineMenu.Application.ViewModel;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    public class AdminController: AppBaseController
    {
        private readonly AdminService adminService;

        public AdminController(AdminService adminService)
        {
            this.adminService = adminService;
        }
        
        [Authorize(Roles.Admin)]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterAdminModel registerAdminModel)
        {
            adminService.Create(registerAdminModel.UserName, registerAdminModel.Password);

            return Created(string.Empty, null);
        }
        
        [HttpPost("authenticate")]
        public ActionResult<AuthenticateAdminResponseModel> Authenticate([FromBody] AuthenticateAdminModel model)
        {
            var authToken = adminService.Authenticate(model.UserName, model.Password);
            
            return Ok(new AuthenticateAdminResponseModel(model.UserName, authToken));
        }
    }
}