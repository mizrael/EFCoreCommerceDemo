using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreCommerceDemo.Example2.Services;

namespace EFCoreCommerceDemo.Example2.Models
{
    public class Order 
    {
        private Order(){ }

        public Order(Guid id, Guid quoteId, DateTime creationDate, IEnumerable<OrderLine> orderLines)
        {
            this.Id = id;
            this.QuoteId = quoteId;
            this.CreationDate = creationDate;
            _orderLines = (orderLines ?? Enumerable.Empty<OrderLine>()).ToList();
        }

        public Guid Id { get; }
        public Guid QuoteId { get;  }
        public DateTime CreationDate { get; }

        public Money GetTotal(ICurrencyConverter currencyConverter, Currency currency)
        {
            var amount = _orderLines.Sum(ol =>
            {
                Money convertedProductPrice = currencyConverter.Convert(ol.Product.Price, currency);
                return convertedProductPrice.Amount * ol.Quantity;
            });
            return new Money(currency, amount);
        }

        private readonly List<OrderLine> _orderLines;
        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

        public static Order FromQuote(Quote quote)
        {
            if(null == quote)
                throw new ArgumentNullException(nameof(quote));

            var orderLines = quote.Items.Select(qi =>
                    new OrderLine(Guid.NewGuid(), qi.Product, qi.Product.Price, qi.Quantity))
                .ToArray();

            return new Order(Guid.NewGuid(), quote.Id, DateTime.UtcNow, orderLines);
        }
    }
}