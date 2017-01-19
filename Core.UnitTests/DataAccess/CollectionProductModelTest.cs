using Core.DataAccess.Collection;
using Core.Entity;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Core.UnitTests.DataAccess
{
    //[the name of the tested method]_[expected input / tested state]_[expected behaviour]
    public class CollectionProductModelTest
    {
        [Trait("Core.Collection", "Unit Test")]
        public void Create_Product_AddsProductToCollectionn()
        {
            //arrange
            CollectionProductModel model = new CollectionProductModel();
            Product product = new Product("p1", "Dog Dinner", 1.20);
            //act
            bool added = model.Create(product);
            //assert
            ICollection<Product> products = model.Products;
            Assert.NotNull(products);
            Assert.Equal(1, products.Count);
            Assert.True(products.Contains(product));
            Assert.True(added);
        }
        [Fact]
        [Trait("Core.Collection", "Unit Test")]
        public void SelectById_ProductId_ReturnsProduct()
        {
            //arrange
            CollectionProductModel model = new CollectionProductModel();
            Product product = new Product("p1", "Dog Dinner", 1.20);
            model.Create(product);
            //act
            Product retrievedProduct = model.SelectById("p1");
            //assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal(product, retrievedProduct);
        }

        [Fact]
        [Trait("Core.Collection", "Unit Test")]
        public void SelectByName_PartOfProductName_ReturnsMatchingProducts()
        {
            //arrange
            CollectionProductModel model = new CollectionProductModel();
            Product product1 = new Product("p1", "Dog Dinner", 1.20);
            Product product2 = new Product("p2", "Fork", 0.4);
            model.Create(product1);
            model.Create(product2);
            //act
            ICollection<Product> products = model.SelectByName("Dog");
            //assert
            Assert.NotNull(products);
            Assert.Equal(1, products.Count);
            Assert.True(products.Contains(product1));
        }
        [Fact]
        [Trait("Core.Collection", "Unit Test")]
        public void Update_Product_ModifiesProductInCollection()
        {
            //arrange
            CollectionProductModel model = new CollectionProductModel();
            Product product = new Product("p1", "Dog Dinner", 1.20);
            Product updatedProduct = new Product("p1", "Dog Dinner", 1.40);
            model.Create(product);
            //act
            bool updated = model.Update(updatedProduct);
            //assert
            Product retrievedProduct = model.SelectById("p1");
            Assert.NotNull(retrievedProduct);
            Assert.Equal(1.40, retrievedProduct.CostPrice);
            Assert.True(updated);
        }
        [Fact]
        [Trait("Core.Collection", "Unit Test")]
        public void Delete_IdOfProduct_RemovesProductFromCollection()
        {
            //arrange
            CollectionProductModel model = new CollectionProductModel();
            Product product = new Product("p1", "Dog Dinner", 1.20);
            model.Create(product);
            //act
            bool deleted = model.Delete("p1");
            //assert
            Product retrievedProduct = model.SelectById("p1");            
            Assert.Null(retrievedProduct);
            Assert.True(deleted);
        }

        [Fact]
        [Trait("Core.Collection", "Unit Test")]
        public void ConstructorCallsReadProductMethodOfSerializer()
        {
            //arrange
            Mock<IProductSerializer> serializer = new Mock<IProductSerializer>();
            serializer.Setup(s => s.ReadProducts()).Returns(new List<Product>());

            //act
            new CollectionProductModel(serializer.Object);

            //assert
            serializer.Verify(s => s.ReadProducts());
        }

        [Fact]
        [Trait("Core.Collection", "Unit Test")]
        public void CreateProductCallsWriteProductMethodOfSerializer()
        {
            //arrange
            Mock<IProductSerializer> serializer = new Mock<IProductSerializer>();
            serializer.Setup(s => s.ReadProducts()).Returns(new List<Product>());
            CollectionProductModel model = new CollectionProductModel(serializer.Object);
            Product product1 = new Product("p1", "Dog Dinner", 1.20);

            //act
            bool created = model.Create(product1);

            //assert
            serializer.Verify(s => s.WriteProducts(It.IsAny<ICollection<Product>>()));
        }
    }
}
