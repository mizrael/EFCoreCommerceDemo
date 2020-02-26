using System;
using System.Linq;
using EFCoreCommerceDemo.Example3.Infrastructure.EntityConfigurations;
using EFCoreCommerceDemo.Example3.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example3.Infrastructure
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();

            this.Seed();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteEntityTypeConfiguration()); 
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderLineEntityTypeConfiguration());
        }

        private void Seed()
        {
            if (!this.Products.Any())
            {
                this.Products.AddRange(Enumerable.Range(1, 100)
                    .Select(i => new Product(Guid.NewGuid(),
                        new Money(Currency.CanadianDollar, 1), $"Product {i}", DateTime.UtcNow)));
            }

            if (!this.Quotes.Any())
            {
                var products = this.Products.ToArray();
                var rand = new Random();

                var quotes = Enumerable.Range(1, 100)
                    .Select(i =>
                    {
                        var q = new Quote(Guid.NewGuid(), DateTime.UtcNow);
                        var qp = products.OrderBy(p => rand.Next())
                            .Take(rand.Next(1, 5));
                        foreach (var p in qp)
                            q.AddProduct(p, rand.Next(1, 3));
                        return q;
                    });
                this.Quotes.AddRange(quotes);
            }

            this.SaveChanges();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}