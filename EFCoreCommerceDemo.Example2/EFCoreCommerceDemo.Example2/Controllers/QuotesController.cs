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
    public class QuotesController : ControllerBase
    {
        private readonly CommerceDbContext _dbContext;
        private readonly ICurrencyConverter _currencyConverter;

        public QuotesController(CommerceDbContext dbContext, ICurrencyConverter currencyConverter)
        {
            _dbContext = dbContext;
            _currencyConverter = currencyConverter;
        }

        [HttpGet, Route("{id:guid}", Name = "quote-details")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var quote = await _dbContext.Quotes
                .Include(q => q.Items)
                .ThenInclude(qi => qi.Product)
                .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
            if (null != quote)
                return Ok(QuoteView.FromModel(quote, _currencyConverter, Currency.CanadianDollar));
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            var quote = new Quote(Guid.NewGuid(), DateTime.UtcNow);
            _dbContext.Quotes.Add(quote);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute("quote-details", new { id = quote.Id }, quote.Id);
        }

        [HttpPut, Route("{id:guid}/items")]
        public async Task<IActionResult> AddProduct([FromRoute]Guid id, [FromBody]AddQuoteItem dto, CancellationToken cancellationToken = default)
        {
            if (null == dto)
                return BadRequest();
            
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId, cancellationToken);
            if (null == product)
                return BadRequest($"invalid product id: {dto.ProductId}"); 
            
            var quote = await _dbContext.Quotes.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
            if (null == quote)
                return BadRequest($"invalid quote id: {id}");

            if(dto.Quantity < 1)
                return BadRequest($"quantity cannot be less than 1");

            quote.AddProduct(product, dto.Quantity);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}