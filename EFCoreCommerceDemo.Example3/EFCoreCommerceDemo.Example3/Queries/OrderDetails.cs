using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EFCoreCommerceDemo.Example3.Models;
using EFCoreCommerceDemo.Example3.Services;

namespace EFCoreCommerceDemo.Example3.Queries
{
    public class OrderDetails
    {
        private OrderDetails(Guid id, Guid quoteId, Money total, IEnumerable<OrderLine> orderLines)
        {
            this.Id = id;
            this.OrderLines = (orderLines ?? Enumerable.Empty<OrderLine>()).ToImmutableArray();
            this.Total = total;
            this.QuoteId = quoteId;
        }

        public Guid Id { get; }
        public Guid QuoteId { get; }
        public IReadOnlyCollection<OrderLine> OrderLines { get; }
        public Money Total { get; }

        public static OrderDetails FromModel(Order order, ICurrencyConverter currencyConverter, Currency currency)
        {
            if (null == order)
                throw new ArgumentNullException(nameof(order));

            var items = order.OrderLines.Select(qi => new OrderLine(qi.Product.Id, qi.Product.Name,
                currencyConverter.Convert(qi.Product.Price, currency), qi.Quantity)).ToArray();

            var total = order.GetTotal(currencyConverter, currency);

            return new OrderDetails(order.Id, order.QuoteId, total, items);
        }
    }
}