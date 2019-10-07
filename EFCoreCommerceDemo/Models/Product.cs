using System;

namespace EFCoreCommerceDemo.Models
{
    public class Product
    {
        public Product(Guid id, decimal price, string name, DateTime creationDate)
        {
            Id = id;
            Price = price;
            Name = name;
            CreationDate = creationDate;
        }

        public Guid Id { get; }
        public decimal Price { get; }
        public string Name { get; }
        public DateTime CreationDate { get; }

        public override string ToString()
        {
            return $"product {Id} : \"{Name}\", costs {Price}";
        }
    }
}