using System;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Example1.Infrastructure;
using EFCoreCommerceDemo.Example1.Models;

namespace EFCoreCommerceDemo.Example1
{
    public class CommerceRepository : IAsyncDisposable
    {
        private readonly CommerceDbContext _dbContext;

        public CommerceRepository(string connStr)
        {
            _dbContext = CreateCommerceDbContext(connStr);
        }

        private static CommerceDbContext CreateCommerceDbContext(string connStr) => DbContextUtils.Create<CommerceDbContext>(connStr, o => new CommerceDbContext(o));

        public async Task CreateProduct(Product product)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("creating product...");
            
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"product {product.Id} created!");

            Console.ResetColor();
        }

        public async Task CreateQuote(Quote quote)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("creating quote...");

            _dbContext.Quotes.Add(quote);
            await _dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"quote {quote.Id} created!");

            Console.ResetColor();
        }

        public async Task UpdateQuote(Quote quote)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("updating quote...");
            
            _dbContext.Quotes.Update(quote);
            await _dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"quote {quote.Id} updated!");

            Console.ResetColor();
        }

        public async Task CreateOrder(Order order)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("creating order...");

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"order {order.Id} created!");

            Console.ResetColor();
        }
        
        public ValueTask DisposeAsync()
        {
            return _dbContext?.DisposeAsync() ?? default;
        }
    }
}