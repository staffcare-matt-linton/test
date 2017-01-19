using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IOrder
    {
        Account Account { get; set; }
        DateTime Date { get; set; }
        ICollection<LineItem> LineItems { get; set; }
        int OrderId { get; set; }
        OrderStatus OrderStatus { get; set; }

        void AddProduct(Product product, int quantity);
        void RemoveProduct(Product product, int quantity);
    }
}