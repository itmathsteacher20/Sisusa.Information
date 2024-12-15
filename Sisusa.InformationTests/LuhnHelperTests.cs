using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.Information;
using System;
using Sisusa.Information.Financial;

namespace Sisusa.InformationTests
{
    [TestClass]
    public class LuhnHelperTests
    {
        [TestMethod]
        [DataRow("0", 0)]
        [DataRow("1", 9)]
        [DataRow("92020861", 5)] // Test example from the conversation
        [DataRow("123456789", 3)] // Random number
        [DataRow("7992739871", 4)] // Valid example from Luhn standard
        public void GetChecksumDigit_ValidInput_ReturnsExpectedChecksum(string payload, int expectedChecksum)
        {
            // Act
            var checksum = LuhnHelper.GetChecksumDigit(long.Parse(payload));

            // Assert
            Assert.AreEqual(expectedChecksum, checksum);
        }

        [TestMethod]
        [DataRow(350925376592634)] // Valid Luhn-compliant number
        [DataRow(79927398713)] // Valid example from Luhn standard
        [DataRow(4111111111111111)] // Example of valid credit card number
        public void ValidateLuhn_ValidNumbers_ReturnsTrue(long payload)
        {
            // Act
            var sum = LuhnHelper.GetSumFromPayload(payload);

            // Assert
            Assert.AreEqual(0, sum % 10);
        }

        [TestMethod]
        [DataRow(920208610)] // Invalid Luhn-compliant number
        [DataRow(79927398710)] // Invalid example from Luhn standard
        [DataRow(4111111111111112)] // Example of invalid credit card number
        public void ValidateLuhn_InvalidNumbers_ReturnsFalse(long payload)
        {
            // Act
            var sum = LuhnHelper.GetSumFromPayload(payload);

            // Assert
            Assert.AreNotEqual(0, sum % 10);
        }

        [TestMethod]
        public void GetChecksumDigit_NegativeInput_ThrowsInvalidOperationException()
        {
            // Arrange
            var payload = -12345;

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => LuhnHelper.GetChecksumDigit(payload));
        }

        [TestMethod]
        public void GetSum_ValidInput_ReturnsExpectedSum()
        {
            // Arrange
            var payload = 7992739871; // Valid example, should have checksum 3

            // Act
            var sum = LuhnHelper.GetSumFromPayload(payload);

            // Assert
            Assert.AreEqual(56, sum); // Without checksum digit
        }
    }
}
