using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Logic.Tests
{
    [TestFixture]
    public class MasterServiceTests
    {
        private IAlgoService algoService;
        private IDataService dataService;
        private IMasterService masterService;

        [OneTimeSetUp]
        public void TestSetup()
        {
            algoService = A.Fake<IAlgoService>();
            dataService = A.Fake<IDataService>();
            masterService = new MasterService(algoService, dataService);
        }

        [Test]
        public void GetDoubleSum_When_invokes_Then_returns_double_sum_from_DataService_list()
        {
            // Arrange
            A.CallTo(() => dataService.GetAllData()).Returns(new List<int> { 1, 2, 3, 4, 5 });
            A.CallTo(() => algoService.DoubleSum(A<List<int>>._)).Returns(30);

            // Act
            int result = masterService.GetDoubleSum();

            //Assert
            Assert.That(result, Is.EqualTo(30));
        }

        [Test]
        public void GetAverage_When_invokes_Then_return_average_value_of_DataService_list()
        {
            // Arrange
            A.CallTo(() => dataService.GetAllData()).Returns(new List<int> { 1, 2, 3, 4, 5 });
            A.CallTo(() => algoService.GetAverage(A<List<int>>._)).Returns(3);
            
            // Act
            var result = masterService.GetAverage();

            //Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GetMaxSquare_When_invokes_Then_return_square_of_max_num_from_DataService_list()
        {
            // Arrange
            A.CallTo(() => dataService.GetAllData()).Returns(new List<int> { 1, 4, 9, 16, 25 });
            A.CallTo(() => dataService.GetMax()).Returns(25);
            A.CallTo(() => algoService.Sqr(A<int>._)).Returns(625.0);

            // Act
            var result = masterService.GetMaxSquare();

            //Assert
            Assert.That(result, Is.EqualTo(625));
        }
        // Exceptions
        [Test]
        public void GetDoubleSum_When_invokes_but_data_absent_Then_throws_InvalidOperationException()
        {
            // Arrange
            A.CallTo(() => dataService.GetAllData()).Returns(null);

            //Assert
            Assert.Throws<InvalidOperationException>(() => masterService.GetDoubleSum());
        }

        [Test]
        public void GetAverage_When_invokes_but_data_absent_Then_returns_zero()
        {
            // Arrange
            A.CallTo(() => dataService.GetAllData()).Returns(null);

            //Assert
            Assert.That(masterService.GetAverage(), Is.EqualTo(0));
        }

        [Test]
        public void GetMaxSquare_When_invokes_but_data_absent_Then_returns_zero()
        {
            // Arrange
            A.CallTo(() => dataService.GetAllData()).Returns(null);

            //Assert
            Assert.That(masterService.GetMaxSquare(), Is.EqualTo(0));
        }

    }
}
