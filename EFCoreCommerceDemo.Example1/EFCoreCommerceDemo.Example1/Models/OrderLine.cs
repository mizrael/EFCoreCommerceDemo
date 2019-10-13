namespace EFCoreCommerceDemo.Example1.Models
{
    public class OrderLine
    {
        private OrderLine() { }
        
        public OrderLine(Product product, decimal price, int quantity)
        {
            if(quantity < 1)
                throw new ArgumentException("quantity has to be at least 1", nameof(quantity));
            Quantity = quantity;
            
            Product = product ?? throw new ArgumentNullException(nameof(product));
            
            Price = price;
        }
        
        public Product Product { get; }
        public decimal Price { get; }
        public int Quantity { get; }
    }
}
