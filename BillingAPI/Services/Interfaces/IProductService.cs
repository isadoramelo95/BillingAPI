using Domain.Entities;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);
        Task<Product?> GetProductById(Guid id);
    }
}
