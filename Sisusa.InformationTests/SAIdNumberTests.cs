using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.Information.Financial;
using Sisusa.Information.Identification;

namespace Sisusa.Information.Tests
{
    [TestClass]
    public class SAIdNumberTests
    {
        [TestMethod]
        [DataRow("9402285008081", true)] // Valid SA ID
        [DataRow("1234567890123", false)] // Invalid SA ID
        [DataRow("9501010000000", false)] // Invalid checksum
        public void IsValidPin_ShouldReturnExpectedResult(string pin, bool expected)
        {
            bool isValid = LuhnChecksum.IsValid(long.Parse(pin));
            Assert.AreEqual(expected, isValid);
        }

        [TestMethod]
        public void Parse_ShouldCreateSAIdNumber_ForValidInput()
        {
            string validPin = "9402285008081";
            SAIdNumber saIdNumber = SAIdNumber.Parse(validPin);

            Assert.AreEqual(Gender.Male, saIdNumber.Gender);
            Assert.AreEqual(CitizenshipStatus.FullCitizen, saIdNumber.CitizenshipStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPinFormatException))]
        public void Parse_ShouldThrowException_ForInvalidInput()
        {
            string invalidPin = "1234567890123";
            SAIdNumber.Parse(invalidPin);
        }

        [TestMethod]
        public void TryParse_ShouldReturnTrue_ForValidInput()
        {
            string validPin = "9402285008081";
            bool result = SAIdNumber.TryParse(validPin, out var saIdNumber);

            Assert.IsTrue(result);
            Assert.IsNotNull(saIdNumber);
        }

        [TestMethod]
        public void TryParse_ShouldReturnFalse_ForInvalidInput()
        {
            string invalidPin = "1234567890123";
            bool result = SAIdNumber.TryParse(invalidPin, out var saIdNumber);

            Assert.IsFalse(result);
            Assert.IsNull(saIdNumber);
        }

        [TestMethod]
        public void Equals_ShouldReturnTrue_ForEqualInstances()
        {
            string pin = "9402285008081";
            var saId1 = SAIdNumber.Parse(pin);
            var saId2 = SAIdNumber.Parse(pin);

            Assert.IsTrue(saId1.Equals(saId2));
        }

        [TestMethod]
        public void Equals_ShouldReturnFalse_ForDifferentInstances()
        {
            var saId1 = SAIdNumber.Parse("9402285008081");
            var saId2 = SAIdNumber.Parse("9501015008087");

            Assert.IsFalse(saId1.Equals(saId2));
        }

        [TestMethod]
        public void ToString_ShouldReturnExpectedFormat()
        {
            var saId = SAIdNumber.Parse("9402285008081");
            string expected = "9402285008081";

            Assert.AreEqual(expected, saId.ToString());
        }


        [TestMethod]
        public void GetGender_ShouldReturnCorrectGender()
        {
            var maleId = SAIdNumber.Parse("9402285008081");
            var femaleId = SAIdNumber.Parse("9402280008086");
            
            

            Assert.AreEqual(Gender.Male, maleId.Gender);
            Assert.AreEqual(Gender.Female, femaleId.Gender);
        }

        [TestMethod]
        public void CitizenshipStatus_ShouldReturnExpectedValue()
        {
            var saId = SAIdNumber.Parse("9402285008081");
            Assert.AreEqual(CitizenshipStatus.FullCitizen, saId.CitizenshipStatus);
        }
    }
}
