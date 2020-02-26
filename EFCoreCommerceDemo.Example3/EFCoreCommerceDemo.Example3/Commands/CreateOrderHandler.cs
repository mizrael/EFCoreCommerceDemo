using System;
using System.Threading;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example3.Infrastructure;
using EFCoreCommerceDemo.Example3.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example3.Commands
{
    public class CreateOrderHandler : MediatR.INotificationHandler<CreateOrder>
    {
        private readonly CommerceDbContext _dbContext;

        public CreateOrderHandler(CommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateOrder command, CancellationToken cancellationToken)
        {
            var quote = await _dbContext.Quotes
                .Include(q => q.Items)
                .ThenInclude(qi => qi.Product).FirstOrDefaultAsync(q => q.Id == command.QuoteId, cancellationToken);
            if (null == quote)
                throw new ArgumentException($"invalid quote id: {command.QuoteId}", nameof(command.QuoteId));

            var order = Order.FromQuote(quote);
            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}