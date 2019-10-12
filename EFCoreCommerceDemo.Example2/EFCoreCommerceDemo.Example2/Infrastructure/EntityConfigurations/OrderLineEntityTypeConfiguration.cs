using EFCoreCommerceDemo.Example2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCommerceDemo.Example2.Infrastructure.EntityConfigurations
{
    internal class OrderLineEntityTypeConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.ToTable("OrderLines", "dbo");

            builder.HasKey(r => r.Id);

            builder.Property(e => e.Quantity);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey("ProductId");

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