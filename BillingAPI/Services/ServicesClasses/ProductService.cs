using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Services.Interfaces;

namespace Services.ServicesClasses
{
    public class ProductService : IProductService
    {
        private readonly BillingDbContext _context;

        public ProductService(BillingDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.NomeDoProduto))
                throw new Exception("Product is required.");

            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id || p.NomeDoProduto == product.NomeDoProduto);

            if (existingProduct != null)
                return existingProduct;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
