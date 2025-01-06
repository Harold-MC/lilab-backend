using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lilab.Api.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IPagedList<User>>> GetAll([FromQuery] UserParamsViewModel filters)
        {
            
            var data = await _userService.GetPagedAsync(filters);
            return Ok(data);
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            var data = await _userService.CreateAsync(user);
            return Ok(data);
        }
        
        [HttpPut]
        public async Task<ActionResult<User>> Update([FromBody] User user)
        {
            var data = await _userService.UpdateAsync(user);
            return Ok(data);
        }
        
    }
}