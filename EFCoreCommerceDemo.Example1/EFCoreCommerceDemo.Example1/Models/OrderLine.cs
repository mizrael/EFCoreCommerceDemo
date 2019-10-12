namespace EFCoreCommerceDemo.Example1.Models
{
    public class OrderLine
    {
        private OrderLine() { }
        public OrderLine(Product product, decimal price, int quantity)
        {
            Price = price;
            Quantity = quantity;
            Product = product;
        }
        public Product Product { get; }
        public decimal Price { get; }
        public int Quantity { get; }
    }
}