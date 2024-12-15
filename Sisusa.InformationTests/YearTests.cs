using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using Sisusa.Information.Time;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class YearTests
    {
        [TestMethod]
        public void Constructor_ValidYear_ReturnsYearInstance()
        {
            var year = new Year(2024);
            Assert.AreEqual(2024, year.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidYearException))]
        public void Constructor_InvalidYear_ThrowsInvalidYearException()
        {
            var year = new Year(10800); // Invalid year
        }

        [TestMethod]
        public void MinValue_Year_Is1940()
        {
            Assert.AreEqual(1900, Year.MinValue.Value);
        }

        [TestMethod]
        public void FromDateTime_ValidDateTime_ReturnsCorrectYear()
        {
            var dateTime = new DateTime(1999, 12, 31);
            var year = Year.FromDateTime(dateTime);
            Assert.AreEqual(1999, year.Value);
        }

        [TestMethod]
        public void FromInt_ValidYear_ReturnsYearInstance()
        {
            var year = Year.FromInt(2020);
            Assert.AreEqual(2020, year.Value);
        }

        [TestMethod]
        public void IsWithinRange_YearWithinRange_ReturnsTrue()
        {
            var year = new Year(2020);
            var minYear = new Year(2000);
            var maxYear = new Year(2030);

            Assert.IsTrue(year.IsWithinRange(minYear, maxYear));
        }

        [TestMethod]
        public void IsWithinRange_YearOutOfRange_ReturnsFalse()
        {
            var year = new Year(1980);
            var minYear = new Year(2000);
            var maxYear = new Year(2030);

            Assert.IsFalse(year.IsWithinRange(minYear, maxYear));
        }

        [TestMethod]
        public void IsLeapYear_LeapYear_ReturnsTrue()
        {
            var year = new Year(2024);
            Assert.IsTrue(year.IsLeapYear());
        }

        [TestMethod]
        public void IsLeapYear_NonLeapYear_ReturnsFalse()
        {
            var year = new Year(2023);
            Assert.IsFalse(year.IsLeapYear());
        }

        [TestMethod]
        public void IsPast_YearInPast_ReturnsTrue()
        {
            var year = new Year(1990);
            var checkYear = new Year(2000);

            Assert.IsTrue(year.IsPast(checkYear));
        }

        [TestMethod]
        public void IsPast_YearInFuture_ReturnsFalse()
        {
            var year = new Year(2020);
            var checkYear = new Year(1990);

            Assert.IsFalse(year.IsPast(checkYear));
        }

        [TestMethod]
        public void ImplicitOperator_ToInt_ReturnsYearValue()
        {
            var year = new Year(2021);
            int yearValue = year;

            Assert.AreEqual(2021, yearValue);
        }

        [TestMethod]
        public void Equals_SameYear_ReturnsTrue()
        {
            var year1 = new Year(2020);
            var year2 = new Year(2020);

            Assert.IsTrue(year1.Equals(year2));
        }

        [TestMethod]
        public void Equals_DifferentYear_ReturnsFalse()
        {
            var year1 = new Year(2020);
            var year2 = new Year(2021);

            Assert.IsFalse(year1.Equals(year2));
        }

        [TestMethod]
        public void LessThanOperator_ValidComparison_ReturnsTrue()
        {
            var year1 = new Year(2010);
            var year2 = new Year(2020);

            Assert.IsTrue(year1 < year2);
        }

        [TestMethod]
        public void GreaterThanOperator_ValidComparison_ReturnsTrue()
        {
            var year1 = new Year(2030);
            var year2 = new Year(2020);

            Assert.IsTrue(year1 > year2);
        }

        [TestMethod]
        public void ToString_ReturnsYearAsString()
        {
            var year = new Year(2024);
            Assert.AreEqual("2024", year.ToString());
        }

        [TestMethod]
        public void GetHashCode_ReturnsConsistentHashCode()
        {
            var year = new Year(2020);
            var hash1 = year.GetHashCode();
            var hash2 = year.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void AddOperator_ReturnsValidYear()
        {
            var year = Year.FromInt(2011);
            var expect = new Year(2016);

            Assert.AreEqual(expect, year + 5);
        }

        [TestMethod]
        public void SubtractOperator_ReturnsValidNumber()
        {
            var year = new Year(2024);
            var year2 = new Year(2001);
            var expDiff = 23;

            Assert.AreEqual(expDiff, year - year2);
        }

        [TestMethod]
        public void ForwardBy_ReturnsValidYear()
        {
            var year = Year.FromInt(2000);
            var exp = Year.FromInt(2024);

            Assert.AreEqual(exp, year.ForwardBy(24));
        }

        [TestMethod]
        public void GoBackBy_ReturnsCorrectYear()
        {
            var year = new Year(2024);
            var exp = new Year(2000);

            Assert.AreEqual(exp, year.GoBackBy(24));
        }
    }
}