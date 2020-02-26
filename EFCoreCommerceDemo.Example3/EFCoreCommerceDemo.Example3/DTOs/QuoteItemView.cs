using System;
using EFCoreCommerceDemo.Example3.Models;

namespace EFCoreCommerceDemo.Example3.DTOs
{
    public class QuoteItemView
    {
        public QuoteItemView(Guid productId, string name, Money price, int quantity)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Guid ProductId { get; }
        public string Name { get; }
        public Money Price { get; }
        public int Quantity { get; }
    }
}