using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sisusa.Information.Identification;
using SwaziPIN = Sisusa.Information.Identification.SwaziPin;
namespace Sisusa.People.Tests
{
    [TestClass]
    public class SwaziPINTests
    {
        static readonly string thePin = "0210052100360";

        // Test for valid PIN parsing
        [TestMethod]
        public void Parse_ValidPin_ReturnsSwaziPIN()
        {
            // Arrange
            string validPin = "0507112100245";  // Example valid Swazi PIN string

            // Act
            SwaziPIN pin = SwaziPIN.Parse(validPin);

            // Assert
            Assert.IsNotNull(pin);
            Assert.AreEqual(2005, pin.DateOfBirth.Year);
            Assert.AreEqual(7, pin.DateOfBirth.Month);
            Assert.AreEqual(11, pin.DateOfBirth.Day);
            Assert.AreEqual(245, pin.SerialNumber);  // Assuming serial number is the last part
        }

        // Test for invalid PIN format (non-numeric characters)
        [TestMethod]
        [ExpectedException(typeof(InvalidPinFormatException))]
        public void Parse_InvalidPinWithNonNumericCharacters_ThrowsInvalidPinFormatException()
        {
            // Arrange
            string invalidPin = "0507AB100245"; // Contains letters

            // Act
            SwaziPIN pin = SwaziPIN.Parse(invalidPin);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPinFormatException))]
        public void Parse_InvalidPinWithBadDate_ThrowsInvalidPinFormatException()
        {
            _ = SwaziPIN.Parse("0802306100511"); //feb never has 30 days
        }

        // Test for PIN with wrong length
        [TestMethod]
        [ExpectedException(typeof(InvalidPinFormatException))]
        public void Parse_InvalidPinWithIncorrectLength_ThrowsInvalidPinFormatException()
        {
            // Arrange
            string invalidPin = "05071100245"; // Length is too short (12 characters)

            // Act
            SwaziPIN pin = SwaziPIN.Parse(invalidPin);

            // Assert is handled by ExpectedException
        }

        // Test for empty PIN string
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_EmptyPin_ThrowsArgumentNullException()
        {
            // Arrange
            string emptyPin = "";

            // Act
            SwaziPIN pin = SwaziPIN.Parse(emptyPin);

            // Assert is handled by ExpectedException
        }


        // Test for Equals method with identical PINs
        [TestMethod]
        public void Equals_IdenticalPins_ReturnsTrue()
        {
            // Arrange
            SwaziPIN pin1 = SwaziPIN.Parse(thePin);
            SwaziPIN pin2 = SwaziPIN.Parse(thePin);

            // Act
            bool result = pin1.Equals(pin2);

            // Assert
            Assert.IsTrue(result);
        }

        // Test for Equals method with different PINs
        [TestMethod]
        public void Equals_DifferentPins_ReturnsFalse()
        {
            // Arrange
           
            string pinStr2 = "0607116100567";
            SwaziPIN pin1 = SwaziPIN.Parse(thePin);
            SwaziPIN pin2 = SwaziPIN.Parse(pinStr2);

            // Act
            bool result = pin1.Equals(pin2);

            // Assert
            Assert.IsFalse(result);
        }
        

        // Test for equality operator with identical PINs
        [TestMethod]
        public void EqualityOperator_IdenticalPins_ReturnsTrue()
        {
            // Arrange
            SwaziPIN pin1 = SwaziPIN.Parse(thePin);
            SwaziPIN pin2 = SwaziPIN.Parse(thePin);

            // Act
            bool result = pin1 == pin2;

            // Assert
            Assert.IsTrue(result);
        }

        // Test for equality operator with different PINs
        [TestMethod]
        public void EqualityOperator_DifferentPins_ReturnsFalse()
        {
            // Arrange
            var otherPin = "9811256105250";
            SwaziPIN pin1 = SwaziPIN.Parse(thePin);
            SwaziPIN pin2 = SwaziPIN.Parse(otherPin);

            // Act
            bool result = pin1 == pin2;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectPin()
        {
            const string pin1Str = "6810236100520";
            const string pin2Str = "0204191120236";
            var pin1 = SwaziPIN.Parse(pin1Str);
            var pin2 = SwaziPIN.Parse(pin2Str);

            Assert.AreEqual(pin1Str, pin1.ToString());
            Assert.AreEqual(pin2Str, pin2.ToString());
        }

        // Test for GetHashCode consistency
        [TestMethod]
        public void GetHashCode_SamePin_ReturnsSameHashCode()
        {
            // Arrange
            SwaziPIN pin1 = SwaziPIN.Parse(thePin);
            SwaziPIN pin2 = SwaziPIN.Parse(thePin);

            // Act
            int hashCode1 = pin1.GetHashCode();
            int hashCode2 = pin2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [TestMethod]
        public void Pin_Properties_AreValid()
        {
            var pin1 = SwaziPIN.Parse("0001282100635");
            var expectDate = new DateOnly(2000, 1, 28);
            var expectedGender = Gender.Female;
            var serial = 635;

            Assert.AreEqual(expectDate, pin1.DateOfBirth);
            Assert.AreEqual(expectedGender, pin1.Gender);
            Assert.AreEqual(serial, pin1.SerialNumber);
        }
    }
}