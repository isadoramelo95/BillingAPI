using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task<Product> GetProductById(Guid id);
    }
}
