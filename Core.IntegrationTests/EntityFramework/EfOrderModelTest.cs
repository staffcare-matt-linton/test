using Core.DataAccess.EntityFramework;
using Core.Entity;
using Core.IntegrationTests.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Xunit;

namespace Core.IntegrationTests
{
    //test classes in the same collection don't run in parallel
    [Collection("Collection 1")]
    public class EfOrderModelTest
    {
        private EfOrderModel sut = new EfOrderModel();
        public EfOrderModelTest()
        {
            string connectionString = new Settings().sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = File.ReadAllText(@"..\..\setup.sql");
                cmd.ExecuteNonQuery();
            }
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void CanAddAndRetrieveAnOrderWithAssociatedAccountAndLineItems()
        {
            //arrange
            Account account1 = new Account("acc1", "John Smith");
            Product product1 = new Product("p1", "Dog's Dinner", 1.50);
            Product product2 = new Product("p2", "Knife", 0.60);
            Order order = new Order();
            order.Account = account1;
            order.AddProduct(product1, 2);
            order.AddProduct(product2, 1);
            //ICollection<LineItem> lineItems = order.LineItems;

            //act
            int id = sut.Create(order);
            Order retrievedOrder = sut[id];

            //assert
            Assert.True(id > 0);
            Assert.Equal(account1, retrievedOrder.Account);
            Assert.Equal(2, retrievedOrder.LineItems.Count);

            retrievedOrder.LineItems.Select(li => li.Product.Name).Contains("Dog's Dinner");
            retrievedOrder.LineItems.Select(li => li.Product.Name).Contains("Knife");

            //using FluentAssertions;
            //lineItems.Should().BeEquivalentTo(retrievedOrder.LineItems);

            //act
            Order firstOrder = sut.Orders.FirstOrDefault();
            //assert
            Assert.NotNull(firstOrder);
            firstOrder.LineItems.Select(li => li.Product.Name).Contains("Dog's Dinner");
            firstOrder.LineItems.Select(li => li.Product.Name).Contains("Knife");
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Create_OrderWithNonExistingAccount_ThrowsInvalidOperationException()
        {
            //arrange
            Account account1 = new Account("acc5", "John Smith");//account not in database
            Product product1 = new Product("p1", "Dog's Dinner", 1.50);
            Product product2 = new Product("p2", "Knife", 0.60);
            Order order = new Order();
            order.Account = account1;
            order.AddProduct(product1, 2);
            order.AddProduct(product2, 1);
            //assert
            Assert.Throws<InvalidOperationException>(() => { sut.Create(order); });
        }
    }
}
