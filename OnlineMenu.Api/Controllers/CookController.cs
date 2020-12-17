using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Api.ViewModel.Authentication;
using OnlineMenu.Application.Services;

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
        public async Task<ActionResult<AuthenticateResponseModel>> Authenticate([FromBody] AuthenticateRequestModel model)
        {
            var authToken = await cookService.AuthenticateAsync(model.UserName, model.Password);
            
            return Ok(new AuthenticateResponseModel(model.UserName, authToken));
        }
    }
}