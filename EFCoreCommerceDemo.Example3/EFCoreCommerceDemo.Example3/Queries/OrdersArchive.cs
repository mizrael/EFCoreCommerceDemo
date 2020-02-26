using System.Collections.Generic;
using EFCoreCommerceDemo.Example3.Models;

namespace EFCoreCommerceDemo.Example3.Queries
{
    public class OrdersArchive : MediatR.IRequest<IReadOnlyCollection<OrderArchiveItem>>
    {
        public OrdersArchive(Currency currency)
        {
            Currency = currency;
        }

        public Currency Currency { get; }
    }
}