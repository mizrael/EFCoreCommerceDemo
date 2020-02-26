using System;

namespace EFCoreCommerceDemo.Example3.Commands
{
    public class CreateOrder : MediatR.INotification
    {
        public CreateOrder(Guid quoteId)
        {
            QuoteId = quoteId;
        }

        public Guid QuoteId { get; }
    }
}
