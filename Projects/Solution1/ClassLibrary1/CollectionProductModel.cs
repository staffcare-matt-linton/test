using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
    public class CollectionProductModel : IProductModel
    {

        private HashSet<Product> products = new HashSet<Product>();
        private IProductSerializer serialiser;

        public CollectionProductModel(IProductSerializer serialiser)
        {
            this.serialiser = serialiser;
            products = new HashSet<Product>(serialiser.ReadProducts());
        }

        public CollectionProductModel()
        {
        }

        public ICollection<Product> Products { get { return products; } }

        public bool Create(Product product)
        {
            // throw new NotImplementedException();

            bool created = products.Add(product);

            if (serialiser != null) serialiser?.WriteProducts(products);

            return created;
      

        }

        public Product SelectById(string v)
        {

            return products.Where(p => p.Id == v).FirstOrDefault();
            
        }

        public ICollection<Product> SelectByName(string v)
        {
            IEnumerable<Product> filteredProducts = from p in products
                                                    where p.Name.Contains(v)
                                                    select p;

            return filteredProducts.ToList();
            
        }

        public bool Update(Product updatedProduct)
        {
            IEnumerable<Product> filteredProducts = from p in products
                                                    where p.Id == updatedProduct.Id
                                                    select p;

            products.Remove(filteredProducts.FirstOrDefault());

            return products.Add(updatedProduct);

            
        }

        public bool Delete(string v)
        {

            IEnumerable<Product> filteredProducts = from p in products
                                                    where p.Id == v
                                                    select p;

            return products.Remove(filteredProducts.FirstOrDefault());
           
            //throw new NotImplementedException();
        }
    }
}