using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.People;
using System;
using Sisusa.Information.Communication;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class EmailAddressTests
    {
        // Test for correct creation of EmailAddress via builder
        [TestMethod]
        public void TestEmailAddressBuilder_CreatesValidEmailAddress()
        {
            // Arrange
            var userName = "john.doe";
            var emailHost = "example.com";

            // Act
            var email = EmailAddress.BuildAddress
                .WithUserName(userName)
                .OnHost(emailHost)
                .TryBuild();

            // Assert
            Assert.AreEqual($"{userName}@{emailHost}", email.ToString());
        }

        // Test invalid email username in EmailAddressBuilder (null or empty)
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEmailAddressBuilder_ThrowsArgumentNullException_WhenUserNameIsEmpty()
        {
            var email = EmailAddress.BuildAddress
                .WithUserName("")
                .OnHost("example.com")
                .TryBuild();
        }

        // Test invalid email host in EmailAddressBuilder (null or empty)
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEmailAddressBuilder_ThrowsArgumentNullException_WhenHostIsEmpty()
        {
            var email = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("")
                .TryBuild();
        }

        // Test for email address equality (case-insensitive)
        [TestMethod]
        public void TestEmailAddress_Equals_ReturnsTrue_ForEqualEmails()
        {
            var email1 = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            var email2 = EmailAddress.BuildAddress
                .WithUserName("John.Doe")
                .OnHost("example.com")
                .TryBuild();

            Assert.IsTrue(email1.Equals(email2));
        }

        // Test for different email addresses (equality)
        [TestMethod]
        public void TestEmailAddress_Equals_ReturnsFalse_ForDifferentEmails()
        {
            var email1 = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            var email2 = EmailAddress.BuildAddress
                .WithUserName("jane.doe")
                .OnHost("example.com")
                .TryBuild();

            Assert.IsFalse(email1.Equals(email2));
        }

        // Test for GetHashCode consistency
        [TestMethod]
        public void TestEmailAddress_GetHashCode_ReturnsConsistentHashCode()
        {
            var email = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            var hashCode1 = email.GetHashCode();
            var hashCode2 = email.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2);
        }

        // Test email parsing with valid address
        [TestMethod]
        public void TestEmailAddress_Parse_ValidEmail()
        {
            var email = EmailAddress.Parse("john.doe@example.com");

            Assert.AreEqual("john.doe@example.com", email.ToString());
        }

        // Test email parsing with invalid address (missing @ symbol)
        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void TestEmailAddress_Parse_InvalidEmail_MissingAtSymbol()
        {
            EmailAddress.Parse("john.doeexample.com");
        }

        // Test email parsing with invalid address (invalid domain)
        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void TestEmailAddress_Parse_InvalidEmail_InvalidDomain()
        {
            EmailAddress.Parse("john.doe@com");
        }

        // Test EmailStringValidator - Invalid email with consecutive dots
        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void TestEmailStringValidator_InvalidEmail_WithConsecutiveDots()
        {
            EmailStringValidator.Validate("john..doe@example.com");
        }

        // Test EmailStringValidator - Valid email
        [TestMethod]
        public void TestEmailStringValidator_ValidEmail()
        {
            EmailStringValidator.Validate("john.doe@example.com");
        }

        // Test implicit conversion from EmailAddress to string
        [TestMethod]
        public void TestEmailAddress_ImplicitConversion_ToString()
        {
            var email = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            string emailString = email;

            Assert.AreEqual("john.doe@example.com", emailString);
        }

        // Test equality operator ==
        [TestMethod]
        public void TestEmailAddress_EqualityOperator_ReturnsTrue_WhenEmailsAreEqual()
        {
            var email1 = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            var email2 = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            Assert.IsTrue(email1 == email2);
        }

        // Test inequality operator !=
        [TestMethod]
        public void TestEmailAddress_InequalityOperator_ReturnsTrue_WhenEmailsAreNotEqual()
        {
            var email1 = EmailAddress.BuildAddress
                .WithUserName("john.doe")
                .OnHost("example.com")
                .TryBuild();

            var email2 = EmailAddress.BuildAddress
                .WithUserName("jane.doe")
                .OnHost("example.com")
                .TryBuild();

            Assert.IsTrue(email1 != email2);
        }
    }
}