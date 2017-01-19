using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MathsTest
    {
        [Fact]
        public void FactorialShouldReturnCorrectValue()
        {
            //maths m = new maths();

            double result = Maths.Factorial(5);

            Assert.Equal(120, result);

        }

        [Fact]
        [Trait("ConsoleApplication", "Unit Test")]
        public void Factorial_NegativeNumber_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => Maths.Factorial(-1));
        }

    }
}
