using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Examples
{
    public class Enumeration
    {
        static void Main(string[] args)
        {
            OrderStatus os = OrderStatus.Dispatched;
            int i = (int)os;
        }
    }

    //public interface ICollection<T>
    //{
    //    void Add(T item);
    //    bool Remove(T item);
    //}
    //class HashSet<T> : ConsoleApplication.Examples.ICollection<T>
    //{
    //    public void Add(T item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Remove(T item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}