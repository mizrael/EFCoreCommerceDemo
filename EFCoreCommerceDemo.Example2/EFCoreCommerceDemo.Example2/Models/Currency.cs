using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EFCoreCommerceDemo.Example2.Models
{
    public class Currency 
    {
        public Currency(string name, string symbol)
        {
            if(string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentNullException(nameof(symbol));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Amount cannot be null or whitespace.", nameof(name));

            Symbol = symbol;
            Name = name;
        }

        public string Name { get; }
        public string Symbol { get; }

        public override bool Equals(object obj)
        {
            return obj is Currency other &&
                   this.Symbol == other.Symbol;
        }

        public override int GetHashCode() => this.Symbol.GetHashCode();

        public override string ToString()
        {
            return this.Symbol;
        }

        #region Factory

        private static readonly IDictionary<string, Currency> _currencies;

        static Currency()
        {
            _currencies = new Dictionary<string, Currency>()
            {
                { Euro.Name, Euro },
                { CanadianDollar.Name, CanadianDollar },
                { USDollar.Name, USDollar },
            };
        }

        public static Currency FromCode(string code)
        {
            if(string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));
            if(!_currencies.ContainsKey(code))
                throw new ArgumentException($"Invalid code: {code}", nameof(code));
            return _currencies[code];
        }
        
        public static Currency Euro => new Currency("EUR", "€");
        public static Currency CanadianDollar => new Currency("CAD", "CA$");
        public static Currency USDollar => new Currency("USD", "US$");

        #endregion Factory
    }
}