using ClassLibrary1;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            IProductModel model = new EfProductModel();
            ICollection<Product> products = model.Products;
            return View(products);
        }

        private IProductModel productModel = new EfProductModel();
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            productModel.Create(product);
            return RedirectToAction("List");
        }
    }
}