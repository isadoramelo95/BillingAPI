using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.RepositoryClasses
{
    public class CustomerRepository
    {
        private readonly BillingDbContext _context;

        public CustomerRepository(BillingDbContext context)
        {
            _context = context;
        }
        public async Task CreateCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
                throw new Exception("Customer name is required");

            await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();
        }
        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }
    }
}
