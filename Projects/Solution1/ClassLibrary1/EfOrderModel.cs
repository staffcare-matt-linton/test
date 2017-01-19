using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.DataAccess.EntityFramework
{
    public class EfOrderModel : IOrderModel
    {
        public int Create(IOrder order)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                order.Account = context.Accounts.Find(order.Account.Id);
                if (order.Account == null)
                    throw new InvalidOperationException("Account doesn't exist in database");
                context.Entry(order).State = EntityState.Added; //All referenced entities will be added

                //To avoid adding duplicate rows to the tables, change state of referenced entities to Unchanged
                context.Entry(order.Account).State = EntityState.Unchanged;
                //This will cause an exception when SaveChanges is called if the Products haven't previously been added to the data store
                order.LineItems.ToList().ForEach(lineItem => context.Entry(lineItem.Product).State = EntityState.Unchanged);
                context.SaveChanges();
                return order.OrderId;
            }
        }

        public ICollection<Order> Orders
        {
            get
            {
                using (EcommerceContext context = new EcommerceContext())
                {
                    //Eager loading of referenced objects
                    return context.Orders
                        .Include(order => order.LineItems.Select(listItem => listItem.Product))
                        .Include(order => order.Account)
                        .ToList();
                }
            }
        }
        public Order this[int id]
        {
            get
            {
                using (EcommerceContext context = new EcommerceContext())
                {
                    //method syntax
                    return context.Orders
                        .Where(order => order.OrderId == id)
                        .Include(order => order.LineItems.Select(listItem => listItem.Product))
                        .Include(order => order.Account)
                        .First();

                    //query syntax
                    //return (from order in context.Orders
                    //        .Include(order => order.LineItems.Select(listItem => listItem.Product))
                    //        .Include(order => order.Account)
                    //        where order.OrderId == id
                    //        select order).FirstOrDefault();
                }
            }
        }
    }
}