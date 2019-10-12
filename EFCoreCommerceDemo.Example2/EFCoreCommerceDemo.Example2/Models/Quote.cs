using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreCommerceDemo.Example2.Services;

namespace EFCoreCommerceDemo.Example2.Models
{
    public class Quote
    {
        public Quote(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;

            _items = new List<QuoteItem>();
        }

        public Guid Id { get; }

        public Money GetTotal(ICurrencyConverter currencyConverter, Currency currency)
        {
            var amount = _items.Sum(ol =>
            {
                Money convertedProductPrice = currencyConverter.Convert(ol.Product.Price, currency);
                return convertedProductPrice.Amount * ol.Quantity;
            });
            return new Money(currency, amount);
        }

        public DateTime CreationDate { get; }

        private readonly List<QuoteItem> _items;
        public IReadOnlyCollection<QuoteItem> Items => _items;

        public void AddProduct(Product product, int quantity)
        {
            if(null == product)
                throw new ArgumentNullException(nameof(product));
            this._items.Add(new QuoteItem(product, quantity));
        }
    }
}