using Core.DataAccess.EntityFramework;
using Core.DataAccess.SQL;
using Core.Entity;
using Core.IntegrationTests.Properties;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Xunit;

namespace Core.IntegrationTests
{
    //test classes in the same collection don't run in parallel
    [Collection("Collection 1")]
    public class SqlOrderModelTest
    {
        private SqlOrderModel sut = new SqlOrderModel();
        public SqlOrderModelTest()
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
        [Trait("Core.Sql", "Integration Test")]
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
            ICollection<LineItem> lineItems = order.LineItems;

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
        [Trait("Core.Sql", "Integration Test")]
        public void CanAddAndRetrieveTwoOrders()
        {
            //arrange
            Account account1 = new Account("acc1", "John Smith");
            Product product1 = new Product("p1", "Dog's Dinner", 1.50);
            Product product2 = new Product("p2", "Knife", 0.60);
            Product product3 = new Product("p3", "Chair", 30);
            Product product4 = new Product("p4", "Table", 60);
            Product product5 = new Product("p5", "Lamp", 30);
            Order order1 = new Order();
            order1.Account = account1;
            order1.AddProduct(product1, 5);
            order1.AddProduct(product2, 6);
            ICollection<LineItem> lineItems1 = order1.LineItems;

            Order order2 = new Order();
            order2.Account = account1;
            order2.AddProduct(product3, 4);
            order2.AddProduct(product4, 1);
            order2.AddProduct(product5, 1);
            ICollection<LineItem> lineItems2 = order2.LineItems;

            //act
            int id1 = sut.Create(order1);
            int id2 = sut.Create(order2);
            Order retrievedOrder1 = sut[id1];
            Order retrievedOrder2 = sut[id2];

            //assert
            Assert.True(id2 > id1);
            Assert.Equal(account1, retrievedOrder1.Account);
            Assert.Equal(2, retrievedOrder1.LineItems.Count);
            Assert.Equal(3, retrievedOrder2.LineItems.Count);

            retrievedOrder1.LineItems.Select(li => li.Product.Name).Contains("Dog's Dinner");
            retrievedOrder1.LineItems.Select(li => li.Product.Name).Contains("Knife");

            retrievedOrder2.LineItems.Select(li => li.Product.Name).Contains("Chair");
            retrievedOrder2.LineItems.Select(li => li.Product.Name).Contains("Table");
            retrievedOrder2.LineItems.Select(li => li.Product.Name).Contains("Lamp");

            //using FluentAssertions;
            //lineItems.Should().BeEquivalentTo(retrievedOrder.LineItems);

            //act
            Order firstOrder = sut.Orders.FirstOrDefault();
            Order lastOrder = sut.Orders.LastOrDefault();
            //assert
            Assert.NotNull(firstOrder);
            firstOrder.LineItems.Select(li => li.Product.Name).Contains("Dog's Dinner");
            firstOrder.LineItems.Select(li => li.Product.Name).Contains("Knife");

            Assert.NotNull(lastOrder);
            lastOrder.LineItems.Select(li => li.Product.Name).Contains("Chair");
            lastOrder.LineItems.Select(li => li.Product.Name).Contains("Table");
            lastOrder.LineItems.Select(li => li.Product.Name).Contains("Lamp");
        }

    }
}
