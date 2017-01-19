using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IOrderModel
    {
        Order this[int id] { get; }

        ICollection<Order> Orders { get; }

        int Create(IOrder order);
    }
}