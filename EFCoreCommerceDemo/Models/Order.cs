using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreCommerceDemo.Models
{
    public class Order
    {
        private Order()
        {
        }

        public Order(Guid id, Guid quoteId, DateTime creationDate, IEnumerable<OrderLine> orderLines)
        {
            this.Id = id;
            this.QuoteId = quoteId;
            this.CreationDate = creationDate;
            _orderLines = (orderLines ?? Enumerable.Empty<OrderLine>()).ToList();
        }

        public Guid Id { get; }
        public Guid QuoteId { get;  }
        public decimal Total => this._orderLines.Sum(ol => ol.Product.Price * ol.Quantity);
        public DateTime CreationDate { get;  }

        private readonly List<OrderLine> _orderLines;
        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

        public override string ToString()
        {
            return $"order {Id} from quote {QuoteId}, {_orderLines.Count} items, total: {Total}";
        }

        public static Order FromQuote(Quote quote)
        {
            if(null == quote)
                throw new ArgumentNullException(nameof(quote));

            var orderLines = quote.Items.Select(qi =>
                    new OrderLine( qi.Product, qi.Product.Price, qi.Quantity))
                .ToArray();

            return new Order(Guid.NewGuid(), quote.Id, DateTime.UtcNow, orderLines);
        }
    }
}