using EFCoreCommerceDemo.Example2.Models;

namespace EFCoreCommerceDemo.Example2.Services
{
    public interface ICurrencyConverter
    {
        Money Convert(Money productPrice, Currency currency);
    }
}