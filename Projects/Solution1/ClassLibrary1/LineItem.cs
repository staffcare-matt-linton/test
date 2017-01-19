namespace ClassLibrary1
{
    public class LineItem
    {
        
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public LineItem()
        { }

        public LineItem(Product p, int quantity)
        {
            Product = p;
            Quantity = quantity;
        }

    }
}