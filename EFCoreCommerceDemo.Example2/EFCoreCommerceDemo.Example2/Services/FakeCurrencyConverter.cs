using EFCoreCommerceDemo.Example2.Models;

namespace EFCoreCommerceDemo.Example2.Services
{
    public class FakeCurrencyConverter : ICurrencyConverter 
    {
        public Money Convert(Money productPrice, Currency currency)
        {
            return new Money(currency, productPrice.Amount); // of course.
        }
    }
}