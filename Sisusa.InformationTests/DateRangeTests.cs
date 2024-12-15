using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sisusa.Information.Time;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class DateRangeTests
    {
        [TestMethod]
        public void Constructor_ValidStartAndEnd_AssignsCorrectly()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);

            // Act
            var range = DateRange.From(startDate).To(endDate);

            // Assert
            Assert.AreEqual(startDate, range.Start);
            Assert.AreEqual(endDate, range.End);
        }

        [TestMethod]
        public void Constructor_StartGreaterThanEnd_SwapsDates()
        {
            // Arrange
            var startDate = new DateTime(2023, 12, 31);
            var endDate = new DateTime(2023, 1, 1);

            // Act
            var range = DateRange.From(startDate).To(endDate);

            // Assert
            Assert.AreEqual(endDate, range.Start);
            Assert.AreEqual(startDate, range.End);
        }

        [TestMethod]
        public void Duration_CorrectlyCalculatesTimeSpan()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var range = DateRange.From(startDate).To(endDate);

            // Act
            var duration = range.Duration;

            // Assert
            Assert.AreEqual(TimeSpan.FromDays(364), duration);
        }

        [TestMethod]
        public void Includes_DateWithinRange_ReturnsTrue()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var range = DateRange.From(startDate).To(endDate);
            var testDate = new DateTime(2023, 6, 15);

            // Act
            var result = range.Includes(testDate);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Includes_DateOutsideRange_ReturnsFalse()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var range = DateRange.From(startDate).To(endDate);
            var testDate = new DateTime(2024, 1, 1);

            // Act
            var result = range.Includes(testDate);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var range = DateRange.From(startDate).To(endDate);

            // Act
            var result = range.ToString();

            // Assert
            Assert.AreEqual($"{startDate} - {endDate}", result);
        }

        [TestMethod]
        public void Equals_TwoIdenticalDateRanges_ReturnsTrue()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var range1 = DateRange.From(startDate).To(endDate);
            var range2 = DateRange.From(startDate).To(endDate);

            // Act
            var result = range1.Equals(range2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_TwoDifferentDateRanges_ReturnsFalse()
        {
            // Arrange
            var range1 = DateRange.From(new DateTime(2023, 1, 1)).To(new DateTime(2023, 12, 31));
            var range2 = DateRange.From(new DateTime(2024, 1, 1)).To(new DateTime(2024, 12, 31));

            // Act
            var result = range1.Equals(range2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OperatorEquality_IdenticalRanges_ReturnsTrue()
        {
            // Arrange
            var range1 = DateRange.From(new DateTime(2023, 1, 1)).To(new DateTime(2023, 12, 31));
            var range2 = DateRange.From(new DateTime(2023, 1, 1)).To(new DateTime(2023, 12, 31));

            // Act
            var result = range1 == range2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OperatorInequality_DifferentRanges_ReturnsTrue()
        {
            // Arrange
            var range1 = DateRange.From(new DateTime(2023, 1, 1)).To(new DateTime(2023, 12, 31));
            var range2 = DateRange.From(new DateTime(2024, 1, 1)).To(new DateTime(2024, 12, 31));

            // Act
            var result = range1 != range2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DateRangeBuilder_CreatesValidDateRange()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);

            // Act
            var range = DateRange.From(startDate).To(endDate);

            // Assert
            Assert.AreEqual(startDate, range.Start);
            Assert.AreEqual(endDate, range.End);
        }
    }
}