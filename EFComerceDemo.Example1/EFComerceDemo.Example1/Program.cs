using System;
using System.Threading.Tasks;
using EFCoreCommerceDemo.Models;

namespace EFCoreCommerceDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connStr =
                "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCoreCommerceDemo.Example1;Trusted_Connection=True;MultipleActiveResultSets=true";
            
            await using var repo = new CommerceRepository(connStr);

            // let's create a bunch of products...

            var product = new Product(Guid.NewGuid(), 42, "lorem", DateTime.UtcNow);
            await repo.CreateProduct(product);
            Console.WriteLine(product);

            var product2 = new Product(Guid.NewGuid(), 71, "ipsum", DateTime.UtcNow);
            await repo.CreateProduct(product2);
            Console.WriteLine(product2);

            var product3 = new Product(Guid.NewGuid(), 28, "dolor", DateTime.UtcNow);
            await repo.CreateProduct(product3);
            Console.WriteLine(product3);

            // here we create a quote with just one product
            var quote = new Quote(Guid.NewGuid(), DateTime.UtcNow);
            quote.AddProduct(product, 1);
            await repo.CreateQuote(quote);
            Console.WriteLine(quote);

            // now we update the quote and add a new product
            quote.AddProduct(product2, 2);
            await repo.UpdateQuote(quote);
            Console.WriteLine(quote);

            // here we go, let's create an order from the quote
            var order = Order.FromQuote(quote);
            await repo.CreateOrder(order);
            Console.WriteLine(order);

            // I changed my mind, let me add another product to the quote...
            quote.AddProduct(product3, 3);
            await repo.UpdateQuote(quote);
            Console.WriteLine(quote);

            // ...and place another order from it
            var order2 = Order.FromQuote(quote);
            await repo.CreateOrder(order2);
            Console.WriteLine(order2);

            // the first order is left untouched
            Console.WriteLine(order);
        }
    }
}
