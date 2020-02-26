using System;
using System.Threading;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example3.Commands;
using EFCoreCommerceDemo.Example3.Models;
using EFCoreCommerceDemo.Example3.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCommerceDemo.Example3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var query = new OrdersArchive(Currency.CanadianDollar);

            var orders = await _mediator.Send(query, cancellationToken);

            if (null != orders)
                return Ok(orders);
            return NotFound();
        }

        [HttpGet, Route("{id:guid}", Name = "order-details")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new OrderById(id, Currency.CanadianDollar);

            var order = await _mediator.Send(query, cancellationToken);
            
            if (null != order)
                return Ok(order);
            return NotFound();
        }

        [HttpPost, Route("quotes/{quoteId:guid}")]
        public async Task<IActionResult> CreateFromQuote([FromRoute] Guid quoteId,
            CancellationToken cancellationToken = default)
        {
            var command = new CreateOrder(quoteId);
            await _mediator.Publish(command, cancellationToken);

            return Accepted();
        }
    }
}