using EFCoreCommerceDemo.Example3.Models;

namespace EFCoreCommerceDemo.Example3.Services
{
    public interface ICurrencyConverter
    {
        Money Convert(Money productPrice, Currency currency);
    }
}