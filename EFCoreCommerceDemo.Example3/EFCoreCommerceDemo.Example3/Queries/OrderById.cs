using System;
using EFCoreCommerceDemo.Example3.Models;

namespace EFCoreCommerceDemo.Example3.Queries
{
    public class OrderById : MediatR.IRequest<OrderDetails>
    {
        public OrderById(Guid orderId, Currency currency)
        {
            OrderId = orderId;
            Currency = currency;
        }

        public Guid OrderId { get; }
        public Currency Currency { get; }
    }
}
