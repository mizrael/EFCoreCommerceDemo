namespace EFCoreCommerceDemo.Example1.Models
{
    public class QuoteItem
    {
        private QuoteItem() { }
        public QuoteItem(Product product, int quantity)
        { 
            Quantity = quantity;
            Product = product;
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}