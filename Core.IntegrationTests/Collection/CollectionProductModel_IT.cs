using Core.DataAccess.Collection;
using Core.Entity;
using System.IO;
using Xunit;

namespace Core.IntegrationTests
{
    public class CollectionProductModelTest
    {
        [Fact]
        [Trait("Core.Collection", "Integration Test")]
        public void JsonSerializationTest()
        {
            File.Delete(@"..\..\products.json");
            CollectionProductModel model = new CollectionProductModel(new JsonProductSerializer());
            Product product1 = new Product("p1", "Dog Dinner", 1.30);
            //serializes product collection
            bool created = model.Create(product1);
            //deserializes product collection
            model = new CollectionProductModel(new JsonProductSerializer());
            //assert
            Assert.True(model.Products.Contains(product1));
        }

        /*
        [Fact]
        [Trait("Category", "Integration Test")]
        public void XmlSerializationTest()
        {
            File.Delete(@"..\..\products.xml");
            CollectionProductModel model = new CollectionProductModel(new XmlProductSerializer());
            Product product1 = new Product("p1", "Dog Dinner", 1.20);
            //serializes product collection
            bool created = model.Create(product1);
            //deserializes product collection
            model = new CollectionProductModel(new XmlProductSerializer());
            //assert
            Assert.True(model.Products.Contains(product1));
        }
        */
    }
}
