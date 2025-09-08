using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;

namespace Repository.RepositoryClass
{
    public class BillingRepository : IRepository<Billing>
    {
        private readonly BillingDbContext _context;

        public BillingRepository(BillingDbContext context)
        {
            _context = context;
        }

        public async Task CreateBilling(Billing billing)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            var customerExists = await _context.Customers.AnyAsync(c => c.Id == billing.CustomerId);
            if (!customerExists)
                throw new Exception($"Customer with id {billing.CustomerId} not found");
            
            foreach (var line in billing.BillingLines)
            {
                var productExists = await _context.Products.AnyAsync(p => p.Id == line.ProductId);
                if (!productExists)
                    throw new Exception($"Product with id {line.ProductId} not found");
            }

            var billingToAdd = new Billing
            {
                CustomerId = billing.CustomerId,
                Currency = billing.Currency,
                TotalAmount = billing.TotalAmount,
                InvoiceNumber = billing.InvoiceNumber,
                Date = billing.Date,
                DueDate = billing.DueDate,
                BillingLines = billing.BillingLines.Select(line => new BillingLine
                {
                    ProductId = line.ProductId,
                    Description = line.Description,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    SubTotal = line.SubTotal
                }).ToList()
            };

            await _context.Billings.AddAsync(billingToAdd);
            await _context.SaveChangesAsync();

            //await _context.Billings.AddAsync(billing);

            //foreach (var line in billing.BillingLines)
            //{
            //    var product = await _context.Products.FindAsync(line.ProductId);
            //    if (product != null)
            //    {
            //        line.Product = product;
            //    }
            //}
            //await _context.SaveChangesAsync();
        }

        public Billing? GetBillingById(int id)
        {
            return _context.Billings
                           .Include(b => b.Customer)
                           .Include(b => b.BillingLines)
                           .ThenInclude(bl => bl.Product)
                .FirstOrDefault(b => b.Id == id);
        }

        public async Task<string> UpdateBillingCurrency(Billing billing)
        {
            var currencyUpdate = await _context.Billings.FindAsync(billing.Id);
            if (currencyUpdate == null)
                return $"Billing with id {billing.Id} not found";

            currencyUpdate.Currency = billing.Currency;
            await _context.SaveChangesAsync();
            return "Currency updated.";
        }

        public async Task DeleteBilling (int id)
        {
            var billing = await _context.Billings
                .Include(b => b.BillingLines)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (billing == null)
                throw new Exception($"Billing with id {id} not found");

            _context.BillingLines.RemoveRange(billing.BillingLines);
            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();
        }
    }
}
