﻿using EFCoreCommerceDemo.Example1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCommerceDemo.Example1.Infrastructure
{
    internal class QuoteEntityTypeConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.ToTable("Quotes", "dbo");

            builder.HasKey(r => r.Id);
            
            builder.Property(e => e.CreationDate);
            builder.Ignore(e => e.Total);
            
            builder.OwnsMany(s => s.Items, b =>
            {
                b.ToTable("QuoteItems", "dbo")
                    .HasKey("Id");
                
                b.Property(e => e.Quantity);

                b.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey("ProductId");
            });

            var navigation = builder.Metadata.FindNavigation(nameof(Quote.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}