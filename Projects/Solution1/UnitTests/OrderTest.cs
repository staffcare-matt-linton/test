using ClassLibrary1;
//using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.UnitTests.Entity
{
    public class OrderTest
    {
        [Fact]
        [Trait("Core.Entities", "Unit Test")]
        public void AddProduct_WhenPassed2Products_ShouldAddOneLineItemContaining2Products()
        {
            //arrange
            Order order = new Order();
            Product product = new Product("p1", "Dog Dinner", 1.20);
            //act
            order.AddProduct(product, 2);
            //assert
            Assert.NotNull(order.LineItems);
            Assert.Equal(1, order.LineItems.Count);
            Assert.Equal(2, order.LineItems.First().Quantity);
            Assert.True(order.LineItems.All(li => li.Product.Id == "p1"));
        }

        [Fact]
        [Trait("Core.Entities", "Unit Test")]
        public void AddProduct_WhenCalledTwiceWithSameProduct_ShouldIncrementLineItemQuantity()
        {
            //arrange
            Order order = new Order();
            Product product1 = new Product("p1", "Dog Dinner", 1.20);
            Product product2 = new Product("p1", "Dog Dinner", 1.20);
            //act
            order.AddProduct(product1, 2);
            order.AddProduct(product2, 2);
            //assert
            Assert.NotNull(order.LineItems);
            Assert.Equal(1, order.LineItems.Count);
            Assert.Equal(4, order.LineItems.First().Quantity);
            Assert.True(order.LineItems.All(li => li.Product.Id == "p1"));
        }

        [Fact]
        [Trait("Core.Entities", "Unit Test")]
        public void RemoveProduct_WhenPassedProductInLineItem_ShouldDecrementLineItemQuantity()
        {
            //arrange
            Order order = new Order();
            order.LineItems = new List<LineItem> {
                new LineItem(new Product("p1", "Dog Dinner", 1.20), 5),
                new LineItem(new Product("p2", "Cutlery", 5.20), 2)
            };
            //act
            order.RemoveProduct(new Product("p1", "Dog Dinner", 1.20), 1);
            //assert
            Assert.NotNull(order.LineItems);
            Assert.Equal(2, order.LineItems.Count);
            Assert.Equal(4, order.LineItems.First(li=>li.Product.Id=="p1").Quantity);
        }

        [Fact]
        [Trait("Core.Entities", "Unit Test")]
        public void RemoveProduct_WhenPassedProductNotInLineItem_ShouldThrowException()
        {
            //arrange
            Order order = new Order();
            order.LineItems = new List<LineItem> {
                new LineItem(new Product("p1", "Dog Dinner", 1.20), 5),
                new LineItem(new Product("p2", "Cutlery", 5.20), 2)
            };

            //act
            //assert
            Assert.Throws<InvalidOperationException>(()=>
                order.RemoveProduct(new Product("p3", "Cat Food", 1.15), 1)
            );
        }

    }
}
