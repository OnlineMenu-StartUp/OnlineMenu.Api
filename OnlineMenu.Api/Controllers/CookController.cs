using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;
using OnlineMenu.Application.Services;
using OnlineMenu.Application.ViewModel;

namespace OnlineMenu.Api.Controllers
{
    public class CookController: AppBaseController
    {
        private readonly CookService cookService;

        public CookController(CookService cookService)
        {
            this.cookService = cookService;
        }
        
        [HttpPost("authenticate")]
        public ActionResult<AuthenticateResponseModel> Authenticate([FromBody] AuthenticateRequestModel model)
        {
            var authToken = cookService.Authenticate(model.UserName, model.Password);
            
            return Ok(new AuthenticateResponseModel(model.UserName, authToken));
        }
    }
}