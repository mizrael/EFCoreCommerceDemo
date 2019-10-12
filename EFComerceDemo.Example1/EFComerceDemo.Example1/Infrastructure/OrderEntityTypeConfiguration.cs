using EFCoreCommerceDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCommerceDemo.Infrastructure
{
    internal class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "dbo");

            builder.HasKey(r => r.Id);

            builder.Property(e => e.CreationDate);
            
            builder.Property(e => e.QuoteId);
            builder.HasOne<Quote>().WithMany().HasForeignKey(e => e.QuoteId);

            builder.Ignore(o => o.Total);

            builder.OwnsMany(s => s.OrderLines, b =>
            {
                b.ToTable("OrderLines", "dbo")
                    .HasKey("Id");
                
                b.Property(e => e.Price);
                b.Property(e => e.Quantity);
                
                b.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey("ProductId");
            });

            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderLines));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}