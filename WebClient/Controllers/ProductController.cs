using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class ProductController : Controller
    {
        private IProductModel productModel = new EfProductModel();

        public ProductController(IProductModel productModel)
        {
            this.productModel = productModel;
        }

        // GET: Product
        public ActionResult List()
        {
            ICollection<Product> products = productModel.Products;
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {          
            productModel.Create(product);
            return RedirectToAction("List");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}