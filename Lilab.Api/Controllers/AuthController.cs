using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using Lilab.Service.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lilab.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost]
        public async Task<ActionResult<LoginResponseViewModel>> Authenticate(LoginViewModel model)
        {
            var result = await _authService.AuthenticateAsync(model);
            return Ok(result);
        }
    }
}