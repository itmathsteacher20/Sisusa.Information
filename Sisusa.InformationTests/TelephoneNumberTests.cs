using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.People;
using System;
using Sisusa.Information.Communication;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class TelephoneNumberTests
    {
        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            var telephone = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);
            Assert.AreEqual("+1123456789", telephone.ToString());
        }

        [TestMethod]
        public void GetInInternationalNumberFormat_ReturnsCorrectFormat()
        {
            var telephone = TelephoneNumber.ForCountry(44).WithPhoneNumber(123456789);
            Assert.AreEqual("+44123456789", telephone.GetInInternationalNumberFormat());
        }

        [TestMethod]
        public void GetCallLink_ReturnsCorrectTelLink()
        {
            var telephone = TelephoneNumber.ForCountry(91).WithPhoneNumber(987654321);
            Assert.AreEqual("tel:+91987654321", telephone.GetCallLink());
        }

        [TestMethod]
        public void IsValidFormat_ValidPhoneNumber_ReturnsTrue()
        {
            Assert.IsTrue(TestIsValidFormat(1, 123456789));
        }

        [TestMethod]
        public void IsValidFormat_InvalidCountryCode_ReturnsFalse()
        {
            Assert.IsFalse(TestIsValidFormat(0, 123456789)); // Invalid country code
        }

        [TestMethod]
        public void IsValidFormat_InvalidPhoneNumberLength_ReturnsFalse()
        {
            Assert.IsFalse(TestIsValidFormat(123, 12345678901234)); // Too long
        }

        [TestMethod]
        public void Constructor_InvalidPhoneNumber_ThrowsException()
        {
            Assert.ThrowsException<InvalidPhoneNumberPhoneNumberFormatException>(() =>
            {
                _ = TelephoneNumber.ForCountry(1125).WithPhoneNumber(1290123);
            });
        }

        [TestMethod]
        public void Constructor_EmergencyNumber_DoesNotThrowException()
        {
            var telephone = new EmergencyTelephoneNumber(112);
            telephone.GetInInternationalNumberFormat();
            Assert.AreEqual(112, telephone.GetLongPhoneNumber());
        }

        [TestMethod]
        public void Equals_SamePhoneNumber_ReturnsTrue()
        {
            var tel1 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);
            var tel2 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);

            Assert.AreEqual(tel1, tel2);
            Assert.IsTrue(tel1.Equals(tel2));
            Assert.IsTrue(tel1 == tel2);
        }

        [TestMethod]
        public void Equals_DifferentPhoneNumbers_ReturnsFalse()
        {
            var tel1 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);
            var tel2 = TelephoneNumber.ForCountry(44).WithPhoneNumber(987654321);

            Assert.AreNotEqual(tel1, tel2);
            Assert.IsFalse(tel1.Equals(tel2));
        }

        [TestMethod]
        public void HashCode_SamePhoneNumber_HasSameHashCode()
        {
            var tel1 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);
            var tel2 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);

            Assert.AreEqual(tel1.GetHashCode(), tel2.GetHashCode());
        }

        [TestMethod]
        public void OperatorEquality_SamePhoneNumber_ReturnsTrue()
        {
            var tel1 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);
            var tel2 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);

            Assert.IsTrue(tel1 == tel2);
        }

        [TestMethod]
        public void OperatorInequality_DifferentPhoneNumbers_ReturnsTrue()
        {
            var tel1 = TelephoneNumber.ForCountry(1).WithPhoneNumber(123456789);
            var tel2 = TelephoneNumber.ForCountry(44).WithPhoneNumber(987654321);

            Assert.IsTrue(tel1 != tel2);
        }

        [TestMethod]
        public void TelNumberBuilder_CreatesValidTelephoneNumber()
        {
            var builder = TelephoneNumber.ForCountry(1);
            var telephone = builder.WithPhoneNumber(123456789);

            Assert.AreEqual("+1123456789", telephone.ToString());
        }

        [TestMethod]
        public void TelNumberBuilder_InvalidCountry_ThrowsException()
        {
            Assert.ThrowsException<InvalidPhoneNumberPhoneNumberFormatException>(() =>
            {
                var builder = TelephoneNumber.ForCountry(0);
                _ = builder.WithPhoneNumber(123456789);
            });
        }

        [TestMethod]
        public void TelNumberBuilder_InvalidPhoneNumber_ThrowsException()
        {
            Assert.ThrowsException<InvalidPhoneNumberPhoneNumberFormatException>(() =>
            {
                var builder = TelephoneNumber.ForCountry(1045); //bad coutnry
                _ = builder.WithPhoneNumber(12345678); // Too long
            });
        }

        // Helper method to access the protected IsValidFormat
        private static bool TestIsValidFormat(uint country, ulong phoneNumber)
        {
            var method = typeof(TelephoneNumber)
                .GetMethod("IsValidFormat", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            return (bool)method.Invoke(null, new object[] { country, phoneNumber });
        }
    }
}

