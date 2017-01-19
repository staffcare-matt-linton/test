using Core.DataAccess.WebApi;
using Core.Entity;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;
using WinClient.Products.ViewModel;
using Xunit;
using System.Linq;

namespace WinClient.Tests.ViewModel
{
    public class ProductsViewModelTest
    {
        private Mock<IAsyncProductModel> doc = new Mock<IAsyncProductModel>();
        private ProductsViewModel sut;

        private bool eventRaised;
        private PropertyChangedEventArgs eventArgs;

        private ICollection<Product> products = new List<Product>
        {
            new Product("p1", "Dog's Dinner", 1.50),
            new Product("p2", "Knife", 0.60)
        };

        //called before each test method
        public ProductsViewModelTest()
        {
            sut = new ProductsViewModel(doc.Object);

            //add a delegate instance to the ViewModel's PropertyChanged event to enable tracking
            ((INotifyPropertyChanged)sut).PropertyChanged +=
                (object sender, PropertyChangedEventArgs e) => { eventRaised = true; eventArgs = e; };
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void SearchPropertySetter_ShouldCallSelectByNameAsync()
        {
            // Arrange
            //doc.Setup(m => m.SelectByNameAsync(It.IsAny<string>()))
            //    .ReturnsAsync(products);

            // Act
            sut.Search = "something";

            // Assert
            doc.Verify(m => m.SelectByNameAsync("something"));
         }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void ProductsProperty_ShouldRaisePropertyChangedEvent()
        {
            // Act
            sut.Products = products;
            // Assert
            Assert.True(eventRaised);
            Assert.Equal("Products", eventArgs.PropertyName);
        }

        //[Fact]
        //[Trait("WinClient", "Unit Test")]
        //public void AddProduct_ShouldPassProductIntoCreateMethod()
        //{
        //    // Arrange
        //    viewModel.Id = "p1";
        //    viewModel.Name = "Dog's Dinner";
        //    viewModel.CostPrice = 1.50;

        //    // Act
        //    viewModel.AddProductCommand.Execute(null);

        //    // Assert
        //    productServiceClientMock.Verify(pm => pm.CreateAsync(It.Is<Product>(p=>p.Id=="p1")));
        //}

        //[Fact]
        //[Trait("WinClient", "Unit Test")]
        //public void DeleteProduct_ShouldPassProductIdIntoDeleteMethod()
        //{
        //    // Arrange
        //    viewModel.SelectedProduct = new Product { Id = "p1" };

        //    // Act
        //    viewModel.DeleteProductCommand.Execute(null);

        //    // Assert
        //    productServiceClientMock.Verify(pm => pm.DeleteAsync("p1"));
        //}

        //[Fact]
        //[Trait("Category", "Unit Test")]
        //public void LoadData_ShouldLoadProducts()
        //{
        //    // Arrange
        //    productServiceClientMock.Setup(m => m.SelectByNameAsync(It.IsAny<string>())).Returns(products);

        //    // Act
        //    viewModel.LoadData();

        //    // Assert
        //    Assert.Equal(2, viewModel.Products.Count);
        //    var product = viewModel.Products.SingleOrDefault(p => p.Id == "p1");
        //    Assert.Equal("Dog's Dinner", product.Name);
        //    product = viewModel.Products.SingleOrDefault(p => p.Id == "p2");
        //    Assert.Equal("Knife", product.Name);  
        //}
        //    [Fact]
        //    [Trait("WinClient", "Unit Test")]
        //    public void SearchPropertySetter_ShouldCallSelectByNameAsync()
        //    {
        //        // Arrange
        //        productServiceClientMock.Setup(m => m.SelectByNameAsync(It.IsAny<string>()))
        //            .ReturnsAsync(products);

        //        // Act
        //        viewModel.Search = "something";

        //        // Assert
        //        Assert.Equal(2, viewModel.Products.Count);
        //        var product = viewModel.Products.SingleOrDefault(p => p.Id == "p1");
        //        Assert.Equal("Dog's Dinner", product.Name);
        //        product = viewModel.Products.SingleOrDefault(p => p.Id == "p2");
        //        Assert.Equal("Knife", product.Name);
        //    }

    }
}
