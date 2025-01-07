using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using Lilab.Service.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lilab.Api.Controllers
{
    [Route("api/access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;
        
        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IPagedList<Access>>> GetAll([FromQuery] AccessParamsViewModel model)
        {
            var data = await _accessService.GetPagedAsync(model);
            return Ok(data);
        }
        
        [HttpPost]
        public async Task<ActionResult<Access>> Create([FromBody] Access access)
        {
            var data = await _accessService.CreateAsync(access);
            return Ok(data);
        }
    }
}