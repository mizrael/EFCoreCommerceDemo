using System;
using System.Threading;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example3.DTOs;
using EFCoreCommerceDemo.Example3.Infrastructure;
using EFCoreCommerceDemo.Example3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommerceDemo.Example3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly CommerceDbContext _dbContext;

        public ProductsController(CommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var products = await _dbContext.Products.ToArrayAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet, Route("{id:guid}", Name = "product-details")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if(null != product)
                return Ok(product);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct dto, CancellationToken cancellationToken = default)
        {
            if (null == dto)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currency = Currency.FromCode(dto.Currency);

            var product = new Product(Guid.NewGuid(), new Money(currency, dto.Price), dto.Name, DateTime.UtcNow);
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return CreatedAtRoute("product-details", new {id = product.Id}, product.Id);
        }
    }
}