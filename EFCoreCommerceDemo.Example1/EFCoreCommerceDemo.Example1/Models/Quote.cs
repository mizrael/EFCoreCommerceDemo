using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreCommerceDemo.Example1.Models
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
        public decimal Total => this._items.Sum(ol => ol.Product.Price * ol.Quantity);
        public DateTime CreationDate { get; }

        private readonly List<QuoteItem> _items;
        public IReadOnlyCollection<QuoteItem> Items => _items;

        public void AddProduct(Product product, int quantity)
        {
            if(null == product)
                throw new ArgumentNullException(nameof(product));
            this._items.Add(new QuoteItem(product, quantity));
        }

        public override string ToString()
        {
            return $"quote {Id}, {_items.Count} items, total: {Total}";
        }
    }
}