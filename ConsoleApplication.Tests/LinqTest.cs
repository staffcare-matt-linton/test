using ConsoleApplication.Examples;
using ConsoleApplication.Solutions;
using System;
using Xunit;

namespace ConsoleApplication.Tests
{
    public class LinqTest
    {      
        /*  
        [Theory]
        [InlineData(1000, 168)]
        [InlineData(10000, 1229)]
        [InlineData(100000, 9592)]
        [InlineData(1000000, 78498)]
        [InlineData(10000000, 664579)]
        [Trait("Category", "Unit Test")]
        public void PrimeCount_Parameterized(int limit, double expected)
        {
            //act
            int actual = Linq.PrimeCount(limit);
            //assert
            Assert.Equal(expected, actual);
        }
        /*
        [Theory]
        [InlineData(1000, 168)]
        [InlineData(10000, 1229)]
        [InlineData(100000, 9592)]
        [InlineData(1000000, 78498)]
        [InlineData(10000000, 664579)]
        [Trait("Category", "Unit Test")]
        public async void PrimeCountAsync_Parameterized(int limit, double expected)
        {
            //act
            int actual = await Linq.PrimeCountAsync(limit);
            //assert
            Assert.Equal(expected, actual);
        }
        */
    }
}
