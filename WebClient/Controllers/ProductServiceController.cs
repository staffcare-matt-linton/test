using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebClient.Controllers
{
    public class ProductServiceController : ApiController
    {
        private IProductModel productModel = new EfProductModel();

        //private IProductModel productModel;
        //public ProductServiceController(IProductModel productModel)
        //{
        //    this.productModel = productModel;
        //}

        // GET: api/productService
        public IHttpActionResult Get()
        {
            ICollection<Product> products = productModel.Products;
            return Ok(products); //return HTTP status code 200 and serialize films as JSON
        }

        // GET: api/productService/id/[p1]
        [Route("api/productService/id/{id}")]
        public IHttpActionResult Get(string id)
        {
            Product product = productModel.SelectById(id);
            if (product == null)
                return NotFound(); //404
            return Ok(product); //200
        }

        // GET: api/productService/[name]
        [Route("api/productService/{partOfName:alpha}")] //intercepts request to api/productService/[characters]        
        public IHttpActionResult GetByTitle(string partOfName)
        {
            ICollection<Product> products = productModel.SelectByName(partOfName);
            return Ok(products); 
        }

        // POST: api/productService
        public IHttpActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400 
            try
            {
                bool added = productModel.Create(product);
                if (!added)
                    return BadRequest("Product already exists");
                return Created(new Uri(Request.RequestUri + product.Id), product);
            }
            catch (Exception e)
            {
                return InternalServerError(e); //500 
            }
        }

        //public IHttpActionResult Post([FromBody]Product product)
        //{
        //        bool added = productModel.Create(product);
        //    return Ok();
        //}

        // PUT: api/productService/5
        public IHttpActionResult Put(string id, [FromBody]Product value)
        {
            try
            {
                Product product = productModel.SelectById(id);
                if (product == null)
                    return NotFound(); //404
                product.Name = value.Name;
                product.CostPrice = value.CostPrice;
                product.RetailPrice = value.RetailPrice;
                productModel.Update(product);
                return Ok(product); //200
            }
            catch (Exception)
            {
                return InternalServerError(); //500 
            }
        }

        // DELETE: api/productService/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                if(productModel.Delete(id))
                    return Ok(); //200
                return NotFound(); //404
            }
            catch (Exception)
            {
                return InternalServerError(); //500 
            }
        }
    }
}
