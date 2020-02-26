using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreCommerceDemo.Example3.Models;
using EFCoreCommerceDemo.Example3.Services;

namespace EFCoreCommerceDemo.Example3.DTOs
{
    public class QuoteView
    {
        private QuoteView(Guid id, QuoteItemView[] items)
        {
            this.Id = id;
            this.Items = items;
        }

        public Guid Id { get; }
        public IReadOnlyCollection<QuoteItemView> Items { get; }

        public static QuoteView FromModel(Quote quote, ICurrencyConverter currencyConverter, Currency currency)
        {
            if(null == quote)
                throw new ArgumentNullException(nameof(quote));

            var items = quote.Items.Select(qi => new QuoteItemView(qi.Product.Id, qi.Product.Name,
                currencyConverter.Convert(qi.Product.Price, currency), qi.Quantity)).ToArray();
            return new QuoteView(quote.Id, items);
        }
    }
}