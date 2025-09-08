using Domain.Entities;

namespace Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(Customer customer);
        Task<Product> GetProductById(Guid id);
    }
}
