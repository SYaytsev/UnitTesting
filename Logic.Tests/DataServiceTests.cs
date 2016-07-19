using NUnit.Framework;
using System;

namespace Logic.Tests
{
    [TestFixture]
    public class DataServiceTests
    {
        IDataService dataService;

        [OneTimeSetUp]
        public void TestSetup()
        {
            dataService = new DataService(3);
        }

        [Test]
        public void ItemsCount_When_added_several_items_Then_returns_items_count()
        {
            // Arrange
            dataService.AddItem(1);
            dataService.AddItem(2);
            int expected = 6;

            // Act
            int result = dataService.ItemsCount;

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void ItemsCount_When_remove_items_Then_decrease_items_count()
        {
            // Arrange
            dataService.AddItem(1);
            dataService.AddItem(2);
            dataService.RemoveAt(1);
            int expected = 7;

            // Act
            int result = dataService.ItemsCount;

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void GetMax_When_invoke_Then_returns_max_member()
        {
            // Arrange
            dataService.AddItem(1);
            dataService.AddItem(2);
            dataService.AddItem(3);
            int expected = 3;

            // Act
            int result = dataService.GetMax();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        // Exceptions
        [Test]
        public void DataService_When_given__negative_num_Then_throw_ArgumentOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new DataService(-3));
        }
        [Test]
        public void GetElementAt_When_try_get_nonexistent_element_Then_throw_ArgumentOutOfRangeException()
        {
            // Arrange
            dataService.AddItem(1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => dataService.GetElementAt(5));
        }
        [Test]
        public void RemoveAt_When_try_remove_nonexistent_element_Then_throw_ArgumentOutOfRangeException()
        {
            // Arrange
            dataService.AddItem(1);
            dataService.RemoveAt(1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => dataService.RemoveAt(7));
        }
    }
}
