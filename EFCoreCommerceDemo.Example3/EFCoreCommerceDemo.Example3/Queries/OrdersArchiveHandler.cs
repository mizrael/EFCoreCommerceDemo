using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example3.Infrastructure;
using EFCoreCommerceDemo.Example3.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example3.Queries
{
    public class OrdersArchiveHandler : MediatR.IRequestHandler<OrdersArchive, IReadOnlyCollection<OrderArchiveItem>>
    {
        private readonly CommerceDbContext _dbContext;
        private readonly ICurrencyConverter _currencyConverter;

        public OrdersArchiveHandler(CommerceDbContext dbContext, ICurrencyConverter currencyConverter)
        {
            _dbContext = dbContext;
            _currencyConverter = currencyConverter;
        }
        
        public async Task<IReadOnlyCollection<OrderArchiveItem>> Handle(OrdersArchive query, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .ToArrayAsync(cancellationToken);
            return orders.Select(o => new OrderArchiveItem(o.Id, o.QuoteId, o.GetTotal(_currencyConverter, query.Currency), o.OrderLines.Count, o.CreationDate))
                         .ToImmutableArray();
        }
    }
}