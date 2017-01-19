using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ShoppingController : Controller
    {
        private IProductModel productModel;
        private IOrderModel orderModel;
        private IOrder order;

        public ViewResult AjaxProductList()
        {
            return View();
        }

        public PartialViewResult _Search(string partOfName)
        {
            ICollection<Product> products = productModel.SelectByName(partOfName);
            return PartialView(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            productModel.Create(product);
            return RedirectToAction("List");
        }

        public ShoppingController(IProductModel productModel, IOrderModel orderModel, IOrder order)
        {
            this.productModel = productModel;
            this.orderModel = orderModel;
            this.order = order;
        }

        public ViewResult AddProduct(string id)
        {
            Product product = productModel.SelectById(id);
            order.AddProduct(product, 1);
            return View("Basket", order.LineItems);
        }

        public ViewResult RemoveProduct(string id)
        {
            Product product = productModel.SelectById(id);
            order.RemoveProduct(product, 1);
            return View("Basket", order.LineItems);
        }

        public ViewResult Purchase()
        {
            orderModel.Create(order);
            return View("Purchase");
        }

        public ShoppingController(IProductModel productModel)
        {
            this.productModel = productModel;
        }



        // GET: Shopping
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string partOfName)
        {
            return View("List", partOfName==null?
                productModel.Products : productModel.SelectByName(partOfName));
        }

        public ViewResult Basket()
        {
            return View("Basket", order.LineItems);
        }
    }
}