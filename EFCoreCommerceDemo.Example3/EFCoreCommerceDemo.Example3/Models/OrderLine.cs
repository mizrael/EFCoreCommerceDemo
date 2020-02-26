using System;

namespace EFCoreCommerceDemo.Example3.Models
{
    public class OrderLine
    {
        private OrderLine(){ }

        public OrderLine(Guid id, Product product, Money price, int quantity)
        {
            Id = id; 
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Quantity = quantity;
            Product = product;
        }

        public Guid Id { get; }
        public Product Product { get; }
        public Money Price { get; }
        public int Quantity { get; }
    }
}