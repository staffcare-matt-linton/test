using ClassLibrary1;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ProductServiceController : ApiController
    {
        
        private IProductModel productModel = new EfProductModel();
        
        public IHttpActionResult Get()
        {
            ICollection<Product> products = productModel.Products;
            return Ok(products);
        }

        public IHttpActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400
            try
            {
                bool added = productModel.Create(product);
                if (!added)
                    return BadRequest("Product already exists");
                //201
                return Created(new Uri(Request.RequestUri + product.Id), product);
            }
            catch (Exception e)
            {
                return InternalServerError(e); //500
            }
        }
    }
}
