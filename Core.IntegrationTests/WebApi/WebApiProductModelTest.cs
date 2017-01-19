using Core.DataAccess.WebApi;
using Core.Entity;
using Core.IntegrationTests.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.IntegrationTests.WebApi
{
    //test classes in the same collection don't run in parallel
    [Collection("Collection 1")]
    public class WebApiProductModelTest
    {
        public WebApiProductModelTest()
        {
            string connectionString = new Settings().sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = File.ReadAllText(@"..\..\setup.sql");
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        [Fact]
        [Trait("Core.WebApi", "Integration Test")]
        public void SelectByName_ReturnsProducts()
        {
            //arrange
            WebApiProductModel sut = new WebApiProductModel();
            //act
            ICollection<Product> products = sut.SelectByName("");
            //assert
            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }

        [Fact]
        [Trait("Core.WebApi", "Integration Test")]
        public void Create_AddsProductToDatabase()
        {
            //arrange
            WebApiProductModel sut = new WebApiProductModel();
            Product product = new Product("p14", "Hammer", 2.20, 4.40);
            //act
            bool created = sut.Create(product);
            //assert
            Assert.True(created);
        }

        /*****************async methods*********/
        [Fact]
        [Trait("Core.WebApi", "Integration Test")]
        public async void SelectByNameAsync_ReturnsProducts()
        {
            //arrange
            WebApiProductModel sut = new WebApiProductModel();
            //act
            ICollection<Product> products = await sut.SelectByNameAsync("");
            //assert
            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }

        [Fact]
        [Trait("Core.WebApi", "Integration Test")]
        public async void CreateAsync_AddsProductToDatabase()
        {
            //arrange
            WebApiProductModel sut = new WebApiProductModel();
            Product product = new Product("p14", "Hammer", 2.20, 4.40);
            //act
            bool created = await sut.CreateAsync(product);
            //assert
            Assert.True(created);
        }
    }
}
