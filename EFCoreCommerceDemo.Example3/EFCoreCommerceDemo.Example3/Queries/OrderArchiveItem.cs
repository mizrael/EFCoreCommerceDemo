using System;
using EFCoreCommerceDemo.Example3.Models;

namespace EFCoreCommerceDemo.Example3.Queries
{
    public class OrderArchiveItem
    {
        public OrderArchiveItem(Guid id, Guid quoteId, Money total, int orderLinesCount, DateTime creationDate)
        {
            this.Id = id;
            this.QuoteId = quoteId;
            this.Total = total;
            OrderLinesCount = orderLinesCount;
            CreationDate = creationDate;
        }

        public Guid Id { get; }
        public Guid QuoteId { get; }
        public Money Total { get; }
        public int OrderLinesCount { get; }
        public DateTime CreationDate { get; }
    }
}