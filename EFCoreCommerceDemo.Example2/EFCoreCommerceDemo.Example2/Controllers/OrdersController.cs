using System;
using System.Threading;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example2.DTOs;
using EFCoreCommerceDemo.Example2.Infrastructure;
using EFCoreCommerceDemo.Example2.Models;
using EFCoreCommerceDemo.Example2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CommerceDbContext _dbContext;
        private readonly ICurrencyConverter _currencyConverter;

        public OrdersController(CommerceDbContext dbContext, ICurrencyConverter currencyConverter)
        {
            _dbContext = dbContext;
            _currencyConverter = currencyConverter;
        }

        [HttpGet, Route("{id:guid}", Name = "order-details")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (null != order)
                return Ok(OrderView.FromModel(order, _currencyConverter, Currency.CanadianDollar));
            return NotFound();
        }

        [HttpPost, Route("quotes/{quoteId:guid}")]
        public async Task<IActionResult> CreateFromQuote([FromRoute] Guid quoteId,
            CancellationToken cancellationToken = default)
        {
            var quote = await _dbContext.Quotes
                .Include(q => q.Items)
                .ThenInclude(qi => qi.Product).FirstOrDefaultAsync(q => q.Id == quoteId, cancellationToken);
            if (null == quote)
                return BadRequest($"invalid quote id: {quoteId}");

            var order = Order.FromQuote(quote);
            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute("order-details", new {id = order.Id}, order.Id);
        }
    }
}