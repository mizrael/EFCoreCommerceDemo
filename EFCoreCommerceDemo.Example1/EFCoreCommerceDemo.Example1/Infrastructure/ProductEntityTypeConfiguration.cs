using EFCoreCommerceDemo.Example1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCommerceDemo.Example1.Infrastructure
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo");

            builder.HasKey(r => r.Id);
            builder.Property(e => e.Id);
            builder.Property(e => e.CreationDate);
            builder.Property(e => e.Price);
            builder.Property(e => e.Name);
        }
    }
}