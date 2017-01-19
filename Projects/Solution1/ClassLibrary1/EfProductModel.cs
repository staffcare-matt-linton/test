using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;
using ClassLibrary1;

namespace Core.DataAccess.EntityFramework
{
    public class EfProductModel : IProductModel
    {
        /// <summary>
        /// Adds a new product to the data store
        /// </summary>
        /// <param name="product"></param>
        /// <returns>false if the product already exists</returns>
        public bool Create(Product product)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                if (context.Products.Find(product.Id) != null)
                    return false; //doesn't add duplicate products
                context.Entry(product).State = EntityState.Added;
                int rowsAdded = context.SaveChanges();
                return rowsAdded == 1;
            }
        }

        public ICollection<Product> Products
        {
            get
            {
                using (EcommerceContext context = new EcommerceContext())
                {
                    var v = context.Products.ToList();
                    return v;
                }
            }
        }


        /// <summary>
        /// retrieves a Product
        /// </summary>
        /// <param name="id">the product's unique key</param>
        /// <returns></returns>
        public Product SelectById(string id)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                return context.Products.Find(id);
            }
        }

        public ICollection<Product> SelectByName(string partOfName)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                return (from p in context.Products
                        where p.Name.Contains(partOfName)
                        select p).ToList();
            }
        }

        public bool Update(Product product)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                try
                {
                    //product = SelectById(product.Id);
                    context.Entry(product).State = EntityState.Modified;

                    int rowsUpdated = context.SaveChanges();
                    return rowsUpdated == 1;
                }
                catch (DbUpdateException e)
                {
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }

                //if (!context.Products.Any(p => p.Id == product.Id))
                //    return false;
                //context.Entry(product).State = EntityState.Modified;
                //int rowsUpdated = context.SaveChanges();
                //return rowsUpdated == 1;
            }
        }
        public bool Delete(Product p)
        {
            return Delete(p.Id);
        }

        public bool Delete(string id)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                Product product = context.Products.Find(id);
                if (product == null)
                    return false;
                context.Entry(product).State = EntityState.Deleted;
                int rowsDeleted = context.SaveChanges();
                return rowsDeleted == 1;
            }
        }

    }
}