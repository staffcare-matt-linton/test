using Moq;
using System.ComponentModel;
using WinClient.Primes.Model;
using WinClient.Primes.ViewModel;
using Xunit;

namespace WinClient.Tests.ViewModel
{
    public class PrimesViewModelTest
    {
        private Mock<IPrimesModel> primesModelMock = new Mock<IPrimesModel>();
        private PrimesViewModel viewModel;

        private bool eventRaised;
        private PropertyChangedEventArgs eventArgs;

        //called before each test method
        public PrimesViewModelTest()
        {
            primesModelMock.Setup(pm => pm.CountAsync(1000)).ReturnsAsync(168);
            primesModelMock.Setup(pm => pm.CountAsync(1000000)).ReturnsAsync(78498);
            viewModel = new PrimesViewModel(primesModelMock.Object);
            
            //add a delegate instance to the ViewModel's PropertyChanged event to enable tracking
            ((INotifyPropertyChanged)viewModel).PropertyChanged +=
                (object sender, PropertyChangedEventArgs e) => { eventRaised = true; eventArgs = e; };
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void ResultProperty_ShouldRaiseEventNamedResult()
        {
            // Act
            viewModel.Result = "168";

            // Assert
            Assert.True(eventRaised);
            Assert.Equal("Result", eventArgs.PropertyName);
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void Constructor_ShouldCallCountAsyncMethodOfIPrimesModel()
        {
            primesModelMock.Verify(pm => pm.CountAsync(1000000));            
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void LimitProperty_SetTo1000_ShouldCallCountAsyncMethodOfPrimesModel()
        {
            // Act
            viewModel.Limit = 1000;
            // Assert
            primesModelMock.Verify(pm => pm.CountAsync(1000));
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void LimitProperty_SetTo1000_ResultPropertyShouldContain168()
        {
            // Act
            viewModel.Limit = 1000;
            // Assert
            Assert.True(viewModel.Result.Contains("168"));
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void Start_ShouldCallCountMethodOfPrimesModel()
        {
            // Arrange
            viewModel.Limit = 1000;

            // Act
            viewModel.StartCommand.Execute(null);

            // Assert
            primesModelMock.Verify(pm => pm.CountAsync(1000));
            Assert.True(eventRaised);
            Assert.Equal("Result", eventArgs.PropertyName);
            Assert.True(viewModel.Result.Contains("168"));
        }

        [Fact]
        [Trait("WinClient", "Unit Test")]
        public void Start_WhenLimitIs1000_ResultPropertyContains168()
        {
            // Arrange
            viewModel.Limit = 1000;

            // Act
            viewModel.StartCommand.Execute(null);

            // Assert
            Assert.True(viewModel.Result.Contains("168"));
        }

    }
}
