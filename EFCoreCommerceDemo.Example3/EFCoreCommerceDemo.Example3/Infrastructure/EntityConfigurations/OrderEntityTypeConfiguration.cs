using EFCoreCommerceDemo.Example3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCommerceDemo.Example3.Infrastructure.EntityConfigurations
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
            
            builder.HasMany(e => e.OrderLines);
        }
    }
}