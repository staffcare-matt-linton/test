using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ClassLibrary1
{
    public class WebApiProductModel : IProductModel
    {
        //private string uri = "http://www.sdineen.uk/api/productService/";
        private string uri = "http://localhost:51067/api/productService/";
        public ICollection<Product> SelectByName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(uri + name).Result;
                IEnumerable<Product> products =
                response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                return products.ToList();
            }
        }
        public bool Create(Product product)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
                client.PostAsJsonAsync(uri, product).Result;
                return response.StatusCode == HttpStatusCode.Created;
            }
        }

        public ICollection<Product> Products
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        
        public bool Delete(string v)
        {
            throw new NotImplementedException();
        }

        public Product SelectById(string v)
        {
            throw new NotImplementedException();
        }

        

        public bool Update(Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
