using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sisusa.Information.Time;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class YearRangeTests
    {
        [TestMethod]
        public void Constructor_ValidStartAndEndYears_AssignsCorrectly()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2020);

            // Act
            var range = YearRange.From(startYear).To(endYear);

            // Assert
            Assert.AreEqual(startYear, range.Start);
            Assert.AreEqual(endYear, range.End);
        }

        [TestMethod]
        public void Constructor_StartGreaterThanEnd_SwapsStartAndEnd()
        {
            // Arrange
            var startYear = Year.FromInt(2020);
            var endYear = Year.FromInt(2000);

            // Act
            var range = YearRange.From(startYear).To(endYear);

            // Assert
            Assert.AreEqual(endYear, range.Start);
            Assert.AreEqual(startYear, range.End);
        }

        [TestMethod]
        public void Length_CorrectlyCalculatesRangeLength()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2010);
            var range = YearRange.From(startYear).To(endYear);

            // Act
            var length = range.Length;

            // Assert
            Assert.AreEqual(10, length);
        }

        [TestMethod]
        public void Includes_YearWithinRange_ReturnsTrue()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2010);
            var range = YearRange.From(startYear).To(endYear);
            var testYear = Year.FromInt(2005);

            // Act
            var result = range.Includes(testYear);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Includes_YearOutsideRange_ReturnsFalse()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2010);
            var range = YearRange.From(startYear).To(endYear);
            var testYear = Year.FromInt(1999);

            // Act
            var result = range.Includes(testYear);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_TwoIdenticalYearRanges_ReturnsTrue()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2010);
            var range1 = YearRange.From(startYear).To(endYear);
            var range2 = YearRange.From(startYear).To(endYear);

            // Act
            var result = range1.Equals(range2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_TwoDifferentYearRanges_ReturnsFalse()
        {
            // Arrange
            var range1 = YearRange.From(Year.FromInt(2000)).To(Year.FromInt(2010));
            var range2 = YearRange.From(Year.FromInt(2011)).To(Year.FromInt(2020));

            // Act
            var result = range1.Equals(range2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2010);
            var range = YearRange.From(startYear).To(endYear);

            // Act
            var result = range.ToString();

            // Assert
            Assert.AreEqual("2000 - 2010", result);
        }

        [TestMethod]
        public void OperatorEquality_TwoIdenticalRanges_ReturnsTrue()
        {
            // Arrange
            var range1 = YearRange.From(Year.FromInt(2000)).To(Year.FromInt(2010));
            var range2 = YearRange.From(Year.FromInt(2000)).To(Year.FromInt(2010));

            // Act
            var result = range1 == range2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OperatorInequality_TwoDifferentRanges_ReturnsTrue()
        {
            // Arrange
            var range1 = YearRange.From(Year.FromInt(2000)).To(Year.FromInt(2010));
            var range2 = YearRange.From(Year.FromInt(2011)).To(Year.FromInt(2020));

            // Act
            var result = range1 != range2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void YearRangeBuilder_CreatesValidYearRange()
        {
            // Arrange
            var startYear = Year.FromInt(2000);
            var endYear = Year.FromInt(2010);

            // Act
            var range = YearRange.From(startYear).To(endYear);

            // Assert
            Assert.AreEqual(startYear, range.Start);
            Assert.AreEqual(endYear, range.End);
        }
    }
}