using System.Threading;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example3.Infrastructure;
using EFCoreCommerceDemo.Example3.Models;
using EFCoreCommerceDemo.Example3.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example3.Queries
{
    public class OrderByIdHandler : MediatR.IRequestHandler<OrderById, OrderDetails>
    {
        private readonly CommerceDbContext _dbContext;
        private readonly ICurrencyConverter _currencyConverter;

        public OrderByIdHandler(CommerceDbContext dbContext, ICurrencyConverter currencyConverter)
        {
            _dbContext = dbContext;
            _currencyConverter = currencyConverter;
        }

        public async Task<OrderDetails> Handle(OrderById query, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefaultAsync(o => o.Id == query.OrderId, cancellationToken);
            return (null != order) ? OrderDetails.FromModel(order, _currencyConverter, Currency.CanadianDollar) : null;
        }
    }
}