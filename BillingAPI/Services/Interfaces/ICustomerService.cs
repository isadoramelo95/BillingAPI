using Domain.Entities;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer?> GetCustomerById(Guid id);
    }
}
