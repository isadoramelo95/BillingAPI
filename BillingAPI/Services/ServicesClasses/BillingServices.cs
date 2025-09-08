using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services.ServicesClasses
{
    public class BillingServices : IBillingService
    {
        private readonly IRepository<Billing> _repository;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        public BillingServices(IRepository<Billing> repository,
           ICustomerService customerService, IProductService productService)
        {
            _repository = repository;
            _customerService = customerService;
            _productService = productService;
        }
        public async Task<List<Billing>> CreateMultipleBillings(List<Billing> billings)
        {
            var results = new List<Billing>();

            foreach (var billing in billings)
            {
                var result = await CreateBilling(billing);
                results.Add(result.First());
            }

            return results;
        }
        public async Task<List<Billing>> CreateBilling(Billing createBilling)
        {
            var customer = await _customerService.GetCustomerById(createBilling.CustomerId);
            if (customer == null)
                throw new Exception($"Customer with id {createBilling.CustomerId} not found. Please create the customer first.");

            var billingLines = new List<BillingLine>();
            foreach (var line in createBilling.BillingLines)
            {
                var product = await _productService.GetProductById(line.ProductId);
                if (product == null)
                    throw new Exception($"Product with id {line.ProductId} not found. Please create the product first.");

                billingLines.Add(new BillingLine
                {
                    ProductId = product.Id,
                    Product = product,
                    Description = string.IsNullOrEmpty(line.Description) ? product.NomeDoProduto : line.Description,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    SubTotal = line.Quantity * line.UnitPrice
                });
            }

            var billing = new Billing
            {
                CustomerId = customer.Id,
                Customer = customer,
                Currency = createBilling.Currency,
                TotalAmount = billingLines.Sum(line => line.SubTotal),
                InvoiceNumber = createBilling.InvoiceNumber,
                Date = createBilling.Date,
                DueDate = createBilling.DueDate,
                BillingLines = billingLines
            };

            await _repository.CreateBilling(billing);
            return new List<Billing> { billing };
        }
        public Billing? GetBillingById(int id) => id <= 0 ? 
            null : _repository.GetBillingById(id);

        public async Task<string> UpdateBillingCurrency(Billing billing) =>
            await _repository.UpdateBillingCurrency(billing);


        public async Task<string> DeleteBilling(int id)
        {
            if (id == null)
                throw new Exception("Id not found.");
            else
            {
                await _repository.DeleteBilling(id);
            }

            return "Billing was deleted.";
        }

    }
}

