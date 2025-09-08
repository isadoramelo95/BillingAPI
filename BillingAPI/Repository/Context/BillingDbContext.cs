using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingLine> BillingLines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.HasOne(b => b.Customer)
                      .WithMany(c => c.Billings)
                      .HasForeignKey(b => b.CustomerId);
            });

            modelBuilder.Entity<BillingLine>(entity =>
            {
                entity.HasKey(bl => bl.Id);
                entity.HasOne(bl => bl.Billing)
                      .WithMany(b => b.BillingLines)
                      .HasForeignKey(bl => bl.BillingId);
                entity.HasOne(bl => bl.Product)
                      .WithMany(p => p.BillingLines)
                      .HasForeignKey(bl => bl.ProductId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.NomeDoProduto).IsRequired();
            });
        }
    }
}
