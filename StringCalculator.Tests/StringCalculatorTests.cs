using System;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void Add_GivenEmptyString_ShouldReturnZero()
        {
            //...............Arrange...................
            var emptyString = string.Empty;
            var stringCalculator = CreateStringCalculator();

            //................Act.......................
            var result = stringCalculator.Add(emptyString);

            //................Assert....................
            var expected = 0;
            Assert.AreEqual(expected, result);

        }

        [TestCase("3", 3)]
        [TestCase("4", 4)]
        [TestCase("5", 5)]
        [TestCase("6", 6)]
        public void Add_GivenSingleNumberAsString_ShouldReturnTheNumber(string inputString, int expected)
        {
            //...............Arrange...................
            var stringCalculator = CreateStringCalculator();

            //................Act.......................
            var result = stringCalculator.Add(inputString);

            //................Assert....................
            Assert.AreEqual(expected, result);

        }

        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("2,3,4", 9)]
        [TestCase("2,3,4,5", 14)]
        [TestCase("2,3,4,5,6", 20)]
        public void Add_Given_CommaSeperatedNumbersAsString_ShouldReturnTheSumOfTheNumbers(string inputString, int expectedSum)
        {
            //...............Arrange...................
            var stringCalculator = CreateStringCalculator();

            //................Act.......................
            var result = stringCalculator.Add(inputString);

            //................Assert....................
            Assert.AreEqual(expectedSum, result);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("1\n3,3", 7)]
        [TestCase("2,3\n5", 10)]
        public void Add_GivenAStringOfNumbersSeperatedByCommandAndNewLine_ShouldUseNewLineAsDelimeterAndReturnSum(string inputString, int expectedSum)
        {
            //...............Arrange...................
            var stringCalculator = CreateStringCalculator();

            //................Act.......................
            var result = stringCalculator.Add(inputString);

            //................Assert....................
            Assert.AreEqual(expectedSum, result);
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//:\n1:8", 9)]
        [TestCase("//*\n9*7", 16)]
        [TestCase("//*\n9***8", 17)]
        public void Add_GivenStringOfNumbersSeparatedByDynamicDelimiter_ShouldUseDelimiterAndReturnSum(string inputString, int expectedSum)
        {
            //...............Arrange...................
            var stringCalculator = CreateStringCalculator();

            //................Act.......................
            var result = stringCalculator.Add(inputString);

            //................Assert....................
            Assert.AreEqual(expectedSum, result);
        }

        [TestCase("//*\n9*-8", "Negatives not allowed -8")]
        [TestCase("//:\n9:-1:", "Negatives not allowed -1")]
        [TestCase("1,-2,3,-4", "Negatives not allowed -2 -4")]
        [TestCase("3,-6,9,-12", "Negatives not allowed -6 -12")]
        public void Add_GivenNegativeNumber_ShouldThrowException(string inputString, string expectedMessage)
        {
            //...............Arrange...................
            var stringCalculator = CreateStringCalculator();
            //................Act.......................
            var exception = Assert.Throws<Exception>(() => stringCalculator.Add(inputString));
            //................Assert....................
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase("//;\n1;1000;2;1001;", 1003)]
        [TestCase("//;\n10;990;1010;", 1000)]
        [TestCase("//;\n11;930;1010;0", 941)]
        public void Add_GivenNumbersGreaterThan1000_ShouldReturSumOfOnlyNumbersLessOrEqualTo1000(string inputString, int expected)
        {
            //...............Arrange...................
            var stringCalculator = CreateStringCalculator();

            //................Act.......................
            var result = stringCalculator.Add(inputString);

            //................Assert....................
            Assert.AreEqual(expected, result);
        }

       
        private Calculator CreateStringCalculator()
        {
            var calculator = new Calculator();
            return calculator;
        }

    }
}
