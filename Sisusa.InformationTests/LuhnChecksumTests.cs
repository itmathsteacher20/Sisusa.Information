using Sisusa.Information.Financial;

namespace Sisusa.InformationTests
{
    [TestClass]
    public class LuhnChecksumTests
    {
        [TestMethod]
        [DataRow(7992739871L, 4, true)] // Valid Luhn-compliant number
        [DataRow(7992739871L, 5, false)] // Incorrect checksum digit
        [DataRow(12345674L, 0, true)] // Valid Luhn-compliant number
        [DataRow(12345674L, 5, false)] // Incorrect checksum digit
        public void IsValid_WithPayloadAndChecksum_ReturnsExpectedResult(long payload, int checksumDigit, bool expected)
        {
            // Act
            var result = LuhnChecksum.IsValid(payload, checksumDigit);
           
            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(79927398713L, true)] // Valid Luhn-compliant number with checksum
        [DataRow(79927398710L, false)] // Invalid Luhn-compliant number with checksum
        [DataRow(4111111111111111L, true)] // Valid example (credit card)
        [DataRow(4111111111111112L, false)] // Invalid example (credit card)
        [DataRow(123456781L, false)] // Incorrect checksum
        [DataRow(123456789L, false)] // Non-compliant number
        public void IsValid_WithFullNumber_ReturnsExpectedResult(long valueToTest, bool expected)
        {
            // Act
            var result = LuhnChecksum.IsValid(valueToTest);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(-79927398713L)] // Negative numbers
        //[DataRow(123.45)] // Decimal input (not valid for the method)
        //[DataRow(0)] // Zero as input
        public void IsValid_WithInvalidInputs_ThrowsException(object valueToTest)
        {
            // Act & Assert
            Assert.ThrowsException<FormatException>(() => LuhnChecksum.IsValid(Convert.ToInt64(valueToTest)));
        }

        [TestMethod]
        public void IsValid_WithEdgeCase_ReturnsExpectedResult()
        {
            // Arrange
            long smallestValid = 0L; // Single-digit Luhn-compliant

            // Act
            var result = LuhnChecksum.IsValid(smallestValid);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
