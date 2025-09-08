using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Services.Interfaces;

namespace Services.ServicesClasses
{
    public class CustomerService : ICustomerService
    {
        private readonly BillingDbContext _context;

        public CustomerService(BillingDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
                throw new Exception("Customer Name is required");

            var existingCustomer = await _context.Customers
               .FirstOrDefaultAsync(c => c.Id == customer.Id || c.Name == customer.Name);

            if (existingCustomer != null)
                return existingCustomer;

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }
    }
}
