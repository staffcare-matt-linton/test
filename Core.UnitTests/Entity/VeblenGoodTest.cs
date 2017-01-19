using Core.Entity;
using Xunit;

namespace Core.UnitTests.Entity
{
    public class VeblenGoodTest
    {
        [Fact]
        [Trait("Core.Entities", "Unit Test")]
        public void RetailPriceShouldBe4TimesCostPrice()
        {
            //arrange
            VeblenGood product = new VeblenGood();
            product.CostPrice = 10;
            //act
            double retailPrice = product.RetailPrice;
            //assert
            Assert.Equal(product.CostPrice * 4, product.RetailPrice);
        }

        [Fact]
        [Trait("Core.Entities", "Unit Test")]
        public void ConstructorSetsProperties()
        {
            //act
            VeblenGood product = new VeblenGood("p4", "Rolex watch", 700);

            //assert
            Assert.Equal("p4", product.Id);
            Assert.Equal("Rolex watch", product.Name);
            Assert.Equal(700, product.CostPrice);
        }
    }
}
