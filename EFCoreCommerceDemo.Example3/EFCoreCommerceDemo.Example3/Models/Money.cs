using System;

namespace EFCoreCommerceDemo.Example3.Models
{
    public class Money 
    {
        private Money() { }
        
        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        public decimal Amount { get; }
        public Currency Currency { get; }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Amount, this.Currency);
        }

        public override bool Equals(object obj)
        {
            return obj is Money other &&
                   this.Amount == other.Amount &&
                   this.Currency == other.Currency;
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}