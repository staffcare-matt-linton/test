using Core.DataAccess;
using Core.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class ShoppingController : Controller
    {
        private IProductModel productModel;
        private IOrderModel orderModel;
        private IOrder order;

        public ShoppingController(IProductModel productModel, IOrderModel orderModel, IOrder order)
        {
            this.productModel = productModel;
            this.orderModel = orderModel;
            this.order = order;
        }

        public ViewResult Index()
        {
            return View();
        }


        //public ViewResult List()
        //{
        //    return View("ProductList", productModel.Products);
        //}

        public ViewResult List(string partOfName)
        {
            return View("ProductList",
                partOfName==null? productModel.Products :
                productModel.SelectByName(partOfName));
        }

        public ViewResult Basket()
        {
            return View(order.LineItems);
        }

        //[Authorize]
        public ViewResult AddProduct(string id)
        {
            Product product = productModel.SelectById(id);
            order.AddProduct(product, 1);
            return View("Basket", order.LineItems);
        }

        public ViewResult Purchase()
        {
            orderModel.Create(order);
            return View("Purchase");
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

        public ViewResult AjaxProductList()
        {
            return View();
        }

        public PartialViewResult _Search(string partOfName)
        {
            ICollection<Product> products = productModel.SelectByName(partOfName);
            return PartialView(products);
        }

    }
}