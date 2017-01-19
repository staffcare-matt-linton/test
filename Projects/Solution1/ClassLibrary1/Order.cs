using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Account Account { get; set; } = new Account("acc2", "Jane Jones");
        public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();

        public OrderStatus OrderStatus { get; set; } = OrderStatus.NotPlaced;

        public void AddProduct(Product product, int quantity)
        {
            IEnumerable<LineItem> query = from li in LineItems where li.Product.Id == product.Id select li;

            LineItem lineItem = query.FirstOrDefault();

            if(lineItem == null)
            {
                LineItems.Add(new LineItem(product, quantity));
            }
            else
            {
                lineItem.Quantity += quantity;
            }
        }

        public void RemoveProduct(Product product, int quantity)
        {

            IEnumerable<LineItem> query = from li in LineItems where li.Product.Id == product.Id select li;

            LineItem lineItem = query.FirstOrDefault();

            if (lineItem == null)
            {
                throw new InvalidOperationException("product not in order");
            }
            else
            {
                lineItem.Quantity -= quantity;
                if (lineItem.Quantity == 0) LineItems.Remove(lineItem);
            }
        }
    }
}
