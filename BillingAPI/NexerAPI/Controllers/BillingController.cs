using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.Context;
using Services.Interfaces;
using Services.ServicesClasses;

namespace NexerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingServices;
        private readonly BillingDbContext _context;
        public BillingController(IBillingService billingServices, BillingDbContext context)
        {
            _billingServices = billingServices ?? throw new ArgumentNullException(nameof(billingServices));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        [HttpPost("multiple")]
        public async Task<IActionResult> PostMutipleBillings([FromBody] List<Billing> billings)
        {
            try
            {
                var results = new List<Billing>();
                foreach (var billing in billings)
                {
                    var result = await _billingServices.CreateBilling(billing);
                    results.Add(billing);
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBilling([FromBody] Billing billingRequest)
        {
            try
            {
                var billing = new Billing
                {
                    CustomerId = billingRequest.CustomerId,
                    Currency = billingRequest.Currency,
                    InvoiceNumber = billingRequest.InvoiceNumber,
                    Date = billingRequest.Date,
                    DueDate = billingRequest.DueDate,
                    BillingLines = billingRequest.BillingLines.Select(line => new BillingLine
                    {
                        ProductId = line.ProductId,
                        Description = line.Description,
                        Quantity = line.Quantity,
                        UnitPrice = line.UnitPrice
                    }).ToList()
                };

                var result = await _billingServices.CreateBilling(billing);
                return Ok(result.First());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var billing = _billingServices.GetBillingById(id);
            if (billing == null)
                return NotFound("Billing not found");

            return Ok(billing);
        }

        [HttpPut("currency")]
        public async Task<IActionResult> UpdateCurrency(int billingId, string currency)
        {
            try
            {
                var billing = new Billing { Id = billingId, Currency = currency };
                var result = await _billingServices.UpdateBillingCurrency(billing);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _billingServices.DeleteBilling(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
