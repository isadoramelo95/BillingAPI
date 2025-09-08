using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Context;
using Services.Interfaces;
using Services.ServicesClasses;

namespace NexerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerServices;
        public CustomerController(ICustomerService customerServices, BillingDbContext context)
        {
            _customerServices = customerServices ?? throw new ArgumentNullException(nameof(customerServices));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                var result = await _customerServices.CreateCustomer(customer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
