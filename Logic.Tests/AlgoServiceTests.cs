using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Tests
{
    [TestFixture]
    public class AlgoServiceTests
    {
        IAlgoService algoService;

        [OneTimeSetUp]
        public void TestSetup()
        {
            algoService = new AlgoService();
        }

        // Different data cases, easier to provide IEnumerable<int>
        public class MyClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new List<int> { 1, 2, 3, 4, 5 }); // .Returns(30) for DoubleSum
                    yield return new TestCaseData(new List<int> { -1000000000, -2000000000 }); // .Returns(-6000000000) for DoubleSum
                    yield return new TestCaseData(new List<int> { 10000000, 20000000 }); // .Returns(60000000) for DoubleSum
                }
            }
        }

        [Test]
        [TestCase(1, 2, ExpectedResult = 6)]
        [TestCase(10000000, 20000000, ExpectedResult = 60000000)]
        public int DoubleSum_When_given_num_collection_Then_returns_double_sum_of_members(int a, int b)
        {
            // Arrange
            IEnumerable<int> arg = new List<int> { a, b };

            return algoService.DoubleSum(arg);
        }

        [Test]
        [TestCaseSource(typeof(MyClass), "TestCases")]
        public void DoubleSum_When_given_num_collection_Then_returns_double_sum_of_members(IEnumerable<int> arg)
        {
            // Arrange
            var expected = arg.Sum(i => i * 2);

            // Act
            int result = algoService.DoubleSum(arg);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DoubleSum_When_given_empty_collection_Then_returns_0()
        {
            IEnumerable<int> arg = new List<int>(2);

            //Assert
            Assert.That(algoService.DoubleSum(arg), Is.EqualTo(0));
        }

        [Test]
        [TestCaseSource(typeof(MyClass), "TestCases")]
        public void MinValue_When_given_num_collection_Then_returns_min_member(IEnumerable<int> arg)
        {
            // Arrange
            int expected = arg.Min(i => i);

            // Act
            int result = algoService.MinValue(arg);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(typeof(MyClass), "TestCases")]
        public void GetAverage_When_given_num_collection_Then_returns_average_of_members(IEnumerable<int> arg)
        {
            // Arrange
            double expected = arg.Average();

            // Acts
            double result = algoService.GetAverage(arg);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(4, ExpectedResult = 16)]
        [TestCase(603729, ExpectedResult = 364488705441)]
        public double Sqr_When_given_num_Then_returns_pow_of_it(int data)
        {
            return algoService.Sqr(data);
        }

        [Test]
        public void MethodsCalledCount_When_methods_calls_Then_gives_number_of_calls()
        {
            // Assert
            Assert.That(algoService.MethodsCalledCount, Is.EqualTo(12));
        }

        // Exceptions

        [Test]
        [TestCase(null)]
        public void DoubleSum_When_given_empty_collection_Then_throws_ArgumentNullException(IEnumerable<int> arg)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => algoService.DoubleSum(arg));
        }

        [Test]
        [TestCase(-1000000000, -2000000000)]
        public void DoubleSum_When_given_num_collection_Then_throws_OverflowException(int a, int b)
        {
            // Arrange
            IEnumerable<int> arg = new List<int> { a, b };

            // Act, Assert
            Assert.That(() => checked(arg.Sum(i => i * 2)),
                Throws.TypeOf<OverflowException>());
        }

        [Test]
        [TestCase(null)]
        public void MinValue_When_given_empty_collection_Then_throws_ArgumentNullException(IEnumerable<int> arg)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => algoService.MinValue(arg));
        }
        [Test]
        [TestCase(null)]
        public void GetAverage_When_given_empty_collection_Then_throws_ArgumentNullException(IEnumerable<int> arg)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => algoService.GetAverage(arg));
        }

        [Test]
        public void MinValue_When_given_empty_collection_Then_throws_InvalidOperationException()
        {
            IEnumerable<int> arg = new List<int>(2);

            //Assert
            Assert.Throws<InvalidOperationException>(() => algoService.MinValue(arg));
        }
        [Test]
        public void GetAverage_When_given_empty_collection_Then_throws_InvalidOperationException()
        {
            IEnumerable<int> arg = new List<int>(2);

            //Assert
            Assert.Throws<InvalidOperationException>(() => algoService.GetAverage(arg));
        }

        [Test]
        [TestCase(-1000000, -3.6, -100000000, -2.5)]
        [TestCase(2140000000, 1, 2140000000, 1)]
        public void Function_When_given_4_diff_nums_Then_throws_OverflowException(int a, double b, int c, double d)
        {
            double result;
            // Assert
            Assert.That(() =>
            result = Math.Pow(d, 3) + checked(a * c) - Math.PI * Math.Sqrt(b),
            Throws.TypeOf<OverflowException>());
        }

    }
}