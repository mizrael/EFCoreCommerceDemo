using EFCoreCommerceDemo.Example3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCommerceDemo.Example3.Infrastructure.EntityConfigurations
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo");

            builder.HasKey(r => r.Id);
            builder.Property(e => e.Id);
            builder.Property(e => e.CreationDate);
            builder.Property(e => e.Name).HasMaxLength(25); 

            builder.OwnsOne(e => e.Price, b =>
            {
                b.Property(e => e.Amount).HasColumnName("Price");
                b.OwnsOne(e => e.Currency, bc =>
                {
                    bc.Property(e => e.Symbol).HasColumnName("CurrencySymbol").HasMaxLength(25);
                    bc.Property(e => e.Name).HasColumnName("CurrencyName").HasMaxLength(25);
                });
            });
        }
    }
}