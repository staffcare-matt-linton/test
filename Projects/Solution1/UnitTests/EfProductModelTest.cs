using ClassLibrary1;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using UnitTests.Properties;
using Xunit;

namespace Core.IntegrationTests
{
    //test classes in the same collection don't run in parallel
    [Collection("Collection 1")]
    public class EfProductModelTest
    {
        private IProductModel sut = new EfProductModel();
        private int productCount;

        public EfProductModelTest()
        {        
            string connectionString = new Settings().sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = File.ReadAllText(@"..\..\setup.sql");
                productCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void ProductsProperty_ReturnsAllProducts()
        {
            //arrange
            //act
            ICollection<Product> products = sut.Products;
            //assert
            Assert.NotNull(products);
            Assert.Equal(productCount, products.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void SelectByName_PartOfProductName_ReturnsMatchingProducts()
        {
            //arrange
            //act
            ICollection<Product> products = sut.SelectByName("Knife");
            //assert
            Assert.NotNull(products);
            Assert.Equal(1, products.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void SelectById_IdOfProduct_MatchingProduct()
        {
            //arrange
            //act
            Product product = sut.SelectById("p4");
            //assert
            Assert.Equal("p4", product.Id);
            Assert.Equal("Spaghetti", product.Name);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Create_Product_ShouldCreateProduct()
        {
            //arrange
            Product product = new Product("p11", "Hammer", 2.20);
            //act
            bool created = sut.Create(product);
            //assert
            Assert.True(created);
            Assert.Equal(productCount+1, sut.Products.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Create_ExistingProduct_ShouldNotCreateProduct()
        {
            //arrange
            Product product = new Product("p1", "Dog's Dinner", 1.50);
            //act
            bool created = sut.Create(product);
            //assert
            Assert.False(created);
            Assert.Equal(productCount, sut.Products.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Update_Product_ShouldUpdateProduct()
        {
            //arrange
            Product product = sut.SelectById("p1"); 
            product.CostPrice = 1.50;
            //act
            bool updated = sut.Update(product);
            //assert
            Assert.True(updated);
            Assert.Equal(1.50, sut.SelectById("p1").CostPrice);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Delete_Product_ShouldRemoveProduct()
        {
            //arrange
            //act
            bool deleted = sut.Delete("p1");
            //assert
            Assert.True(deleted);
            Assert.Null(sut.SelectById("p1"));
            Assert.Equal(productCount-1, sut.Products.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Delete_ProductNotInDataStore_ShouldReturnFalse()
        {
            //arrange
            //act
            bool deleted = sut.Delete("z1");
            //assert
            Assert.False(deleted);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Update_ContrivedOptimisticConcurrencyConflict()
        {
            //arrange
            Product product1 = sut.SelectById("p1");         
            Product product2 = sut.SelectById("p1");
            product1.RetailPrice = 1.50;
            product2.RetailPrice = 1.55;

            //act

            //modifying a row will update the RowVersion column value
            bool updatedProduct1 = sut.Update(product1);

            //product1 RowVersion property and database RowVersion  
            //column values are now unequal, so the update will fail
            bool updatedProduct2 = sut.Update(product2);

            //assert
            Assert.True(updatedProduct1); 
            Assert.False(updatedProduct2); 
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void ShouldStoreAndRerieveVeblenGood()
        {
            //arrange
            VeblenGood product = new VeblenGood
            {
                Id = "v1",
                Name = "Rolex watch",
                CostPrice = 700
            };
            //act
            sut.Create(product);
            Product retrievedProduct = sut.SelectById("v1");
            //assert
            Assert.IsType(typeof(VeblenGood), retrievedProduct);
        }
    }
}
