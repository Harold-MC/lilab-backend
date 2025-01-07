using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lilab.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IPagedList<User>>> GetAll([FromQuery] CustomerParamsViewModel filters)
        {
            var data = await _customerService.GetPagedAsync(filters);
            return Ok(data);
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] Customer customer)
        {
            var data = await _customerService.CreateAsync(customer);
            return Ok(data);
        }
        
        [HttpPut]
        public async Task<ActionResult<User>> Update([FromBody] Customer customer)
        {
            var data = await _customerService.UpdateAsync(customer);
            return Ok(data);
        }
        
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<bool>> Delete(Guid customerId)
        {
            var data = await _customerService.RemoveAsync(customerId);
            return Ok(data);
        }
        
    }
}