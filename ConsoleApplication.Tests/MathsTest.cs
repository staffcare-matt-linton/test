using ConsoleApplication.Solutions;
using System;
using Xunit;

namespace ConsoleApplication.Tests
{
    public class MathsTest
    {
        [Fact]
        [Trait("ConsoleApplication", "Unit Test")]
        public void Factorial_ShouldReturnCorrectValues()
        {
            //arrange

            //act
            double actual = Maths.Factorial(5);
            //assert
            Assert.Equal(120, actual);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(6, 720)]
        [Trait("ConsoleApplication", "Unit Test")]
        public void Factorial_ParameterizedTest(int n, double expected)
        {
            //act
            double actual = Maths.Factorial(n);
            //assert
            Assert.Equal(expected, actual);
        }

        //http://www.onlineconversion.com/roman_numerals_advanced.htm
        [Theory]
        [InlineData(4, "IV")]
        [InlineData(27, "XXVII")]
        [InlineData(109, "CIX")]
        [InlineData(486, "CDLXXXVI")]
        [InlineData(2016, "MMXVI")]
        [InlineData(4999, "MMMMCMXCIX")]
        [InlineData(-1, "")]
        [InlineData(5000, "")]
        [Trait("ConsoleApplication", "Unit Test")]
        public void ToRoman_Arabic_Roman(int n, string expected)
        {
            //act
            string actual = Maths.ToRoman(n);
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("ConsoleApplication", "Unit Test")]
        public void Factorial_NegativeNumber_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => Maths.Factorial(-1));
        }

        [Fact]
        [Trait("ConsoleApplication", "Unit Test")]
        public void Fibonacci_NumberOfElements_ShouldReturnCorrectSequence()
        {
            int[] expected = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            int[] actual = Maths.Fibonacci(10);
            Assert.Equal(expected, actual);
        }
    }
}
