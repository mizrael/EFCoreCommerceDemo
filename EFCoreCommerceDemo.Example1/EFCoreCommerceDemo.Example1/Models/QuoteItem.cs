namespace EFCoreCommerceDemo.Example1.Models
{
    public class QuoteItem
    {
        private QuoteItem() { }
        public QuoteItem(Product product, int quantity)
        { 
            if(quantity < 1)
                throw new ArgumentException("quantity has to be at least 1", nameof(quantity));
            Quantity = quantity;
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}
