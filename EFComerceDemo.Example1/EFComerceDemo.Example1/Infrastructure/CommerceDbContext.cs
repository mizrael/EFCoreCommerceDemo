using EFCoreCommerceDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Infrastructure
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteEntityTypeConfiguration()); 
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}