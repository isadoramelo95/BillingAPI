using Domain.Entities;
using Repository.Context;

namespace Repository.RepositoryClasses
{
    public class ProductRepository
    {
        private readonly BillingDbContext _context;

        public ProductRepository(BillingDbContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.NomeDoProduto))
                throw new Exception("Product's name is required");

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

    }
}
