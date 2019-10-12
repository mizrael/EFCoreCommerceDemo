using System;
using EFCoreCommerceDemo.Example2.Models;

namespace EFCoreCommerceDemo.Example2.DTOs
{
    public class OrderLineView
    {
        public OrderLineView(Guid productId, string name, Money price, int quantity)
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