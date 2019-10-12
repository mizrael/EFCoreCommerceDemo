using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreCommerceDemo.Example2.Models;
using EFCoreCommerceDemo.Example2.Services;

namespace EFCoreCommerceDemo.Example2.DTOs
{
    public class OrderView
    {
        private OrderView(Guid id, OrderLineView[] orderLines)
        {
            this.Id = id;
            this.OrderLines = orderLines;
        }

        public Guid Id { get; }
        public IReadOnlyCollection<OrderLineView> OrderLines { get; }

        public static OrderView FromModel(Order order, ICurrencyConverter currencyConverter, Currency currency)
        {
            if (null == order)
                throw new ArgumentNullException(nameof(order));

            var items = order.OrderLines.Select(qi => new OrderLineView(qi.Product.Id, qi.Product.Name,
                currencyConverter.Convert(qi.Product.Price, currency), qi.Quantity)).ToArray();
            return new OrderView(order.Id, items);
        }
    }
}