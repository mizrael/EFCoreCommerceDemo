using System;

namespace EFCoreCommerceDemo.Example3.Models
{
    public class Product
    {
        private Product(){}

        public Product(Guid id, Money price, string name, DateTime creationDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            this.Id = id;
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Name = name;
            CreationDate = creationDate;
        }

        public Guid Id { get; }
        public Money Price { get; }
        public string Name { get; }
        public DateTime CreationDate { get; }

        public override string ToString()
        {
            return $"product {Id} : \"{Name}\", costs {Price}";
        }
    }
}