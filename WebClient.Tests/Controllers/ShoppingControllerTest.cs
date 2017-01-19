using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Xunit;
using WebClient.Controllers;
using Core.DataAccess;
using Core.Entity;

namespace Store.WebClient.Tests.Controllers
{
    public class ShoppingControllerTest
    {
        private ShoppingController controller;

        private Mock<IProductModel> productModel;
        private Mock<IOrderModel> orderModel;
        private Mock<IOrder> order;

        public ShoppingControllerTest()
        {
            productModel = new Mock<IProductModel>();
            orderModel = new Mock<IOrderModel>();
            order = new Mock<IOrder>();

            controller = new ShoppingController(
                productModel.Object, orderModel.Object, order.Object);
        }

        //[the name of the tested method]_[expected input / tested state]_[expected behavior]. 
        [Fact]
        [Trait("WebClient", "Unit Test")]
        public void List_ReturnsViewNamedProductList()
        {
            // Arrange
            // Act
            ViewResult result = controller.List(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ProductList", result.ViewName);
        }

        //[the name of the tested method]_[expected input / tested state]_[expected behavior]. 
        [Fact]
        [Trait("WebClient", "Unit Test")]
        public void List_CallsSelectByNameMethodOfProductModel()
        {
            // Arrange
            // Act
            ViewResult result = controller.List("partOfTitle");

            // Assert
            productModel.Verify(pm => pm.SelectByName("partOfTitle"));
        }

        //[the name of the tested method]_[expected input / tested state]_[expected behavior]. 
        [Fact]
        [Trait("WebClient", "Unit Test")]
        public void List_PassesCollectionOfProductsToView()
        {
            // Arrange
            Product[] productCollection = {
                new Product("p1", "Dog's Dinner", 1.50),
                new Product("p2", "Knife", 0.60) };

            productModel.Setup(pm => pm.Products).Returns(productCollection);
            
            // Act
            ViewResult result = controller.List(It.IsAny<string>());

            // Assert
            var products = result.ViewData.Model as ICollection<Product>;
            Assert.Equal(2, products.Count);
        }

        /// <summary>
        /// Moq and other similar mocking frameworks can only mock interfaces, 
        /// abstract methods/properties (on abstract classes) or 
        /// virtual methods/properties on concrete classes.
        /// This is because it generates a proxy that will implement the interface 
        /// or create a derived class that overrides those overrideable methods 
        /// in order to intercept calls.
        /// </summary>
        [Fact]
        [Trait("WebClient", "Unit Test")]
        public void Basket_ReturnsListItemsInOrder()
        {
            // Arrange
            LineItem[] lineItemCollection =
            {
                new LineItem(new Product("p1", "Dog's Dinner", 1.50), 2),
                new LineItem(new Product("p2", "Knife", 0.60), 1)
            };

            //LineItems must be a virtual or abstract property
            order.Setup(o => o.LineItems).Returns(lineItemCollection);

            // Act
            ViewResult result = controller.Basket();

            // Assert
            var lineItems = result.ViewData.Model as ICollection<LineItem>;
            Assert.Equal(2, lineItems.Count);
        }

        [Fact]
        [Trait("WebClient", "Unit Test")]
        public void AddProduct_CallsAddProductMethodOfOrder()
        {
            // Arrange
            Product product = new Product("p1", "Dog's Dinner", 1.50);
            productModel.Setup(pm => pm.SelectById("p1")).Returns(product);

            // Act
            ViewResult result = controller.AddProduct("p1"); 

            // Assert
            order.Verify(o => o.AddProduct(product, 1));//must be virtual of abstract method
        }

        [Fact]
        [Trait("WebClient", "Unit Test")]
        public void Pucrhase_CallsCreateMethodOfOrderModel()
        {
            // Act
            ViewResult result = controller.Purchase();

            // Assert
            orderModel.Verify(om => om.Create(order.Object));
        }

        //[TestMethod]
        //[TestCategory("Unit Test")]
        //public void AddProduct_PassesCollectionOfLineItemsToView()
        //{
        //    // Arrange
        //    Product[] productCollection = {
        //        new Product("p1", "Dog's Dinner", 1.50),
        //        new Product("p2", "Knife", 0.60) };

        //    productModel.Setup(pm => pm.Products).Returns(productCollection);

        //    // Act
        //    ViewResult result = controller.List();

        //    // Assert
        //    LineItem[] lineItemCollection =
        //    {
        //        new LineItem(new Product("p1", "Dog's Dinner", 1.50), 2),
        //        new LineItem(new Product("p2", "Knife", 0.60), 1)
        //    };

        //    order.Setup(o => o.LineItems).Returns(lineItemCollection);

        //    var lineItems = result.ViewData.Model as ICollection<LineItem>;
        //    Assert.Equal(2, lineItems.Count);
        //}
    }
}
