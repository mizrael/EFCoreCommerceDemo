using EFCoreCommerceDemo.Example3.Models;

namespace EFCoreCommerceDemo.Example3.Services
{
    public class FakeCurrencyConverter : ICurrencyConverter 
    {
        public Money Convert(Money productPrice, Currency currency)
        {
            return new Money(currency, productPrice.Amount); // of course.
        }
    }
}