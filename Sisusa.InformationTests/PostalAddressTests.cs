using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.Information.Communication;
using System;

namespace Sisusa.Information.Communication.Tests
{
    [TestClass]
    public class PostalAddressTests
    {
        private PostalAddress.PostalAddressBuilder _builder;

        [TestInitialize]
        public void Setup()
        {
            _builder = new PostalAddress.PostalAddressBuilder()
                .WithPrefix("P.O.")
                .WithBoxNumber(123)
                .WithPostalCode("45678")
                .WithTownOrCity("Testville")
                .WithCountry("Testland");
        }

        [TestMethod]
        public void PostalAddress_Equals_ShouldReturnTrueForSameValues()
        {
            var address1 = _builder.Build();
            var address2 = _builder.Build();

            Assert.IsTrue(address1 == address2);
            Assert.IsTrue(address1.Equals(address2));
            Assert.AreEqual(address1.GetHashCode(), address2.GetHashCode());
        }

        [TestMethod]
        public void PostalAddress_Equals_ShouldReturnFalseForDifferentValues()
        {
            var address1 = _builder.Build();
            var address2 = _builder.WithBoxNumber(456).Build();

            Assert.IsFalse(address1 == address2);
            Assert.IsFalse(address1.Equals(address2));
            Assert.AreNotEqual(address1.GetHashCode(), address2.GetHashCode());
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldThrowExceptionForInvalidBoxNumber()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _builder.WithBoxNumber(0).Build());
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldThrowExceptionForEmptyPostalCode()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _builder.WithPostalCode("").Build());
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldThrowExceptionForEmptyCountry()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _builder.WithCountry("").Build());
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldDefaultPrefixToPO()
        {
            var address = _builder.WithPrefix("").Build();
            Assert.AreEqual("P.O.", address.Prefix);
        }

        [TestMethod]
        public void PostalAddress_GetFullAddress_ShouldIncludeAddressToWhenProvided()
        {
            var address = _builder.AddressedTo("John Doe").Build();

            string expected = "John Doe\nP.O. 123, 45678 Testville, Testland";
            Assert.AreEqual(expected, address.GetFullAddress());
        }

        [TestMethod]
        public void PostalAddress_GetFullAddress_ShouldNotIncludeAddressToWhenNotProvided()
        {
            var address = _builder.Build();

            string expected = "P.O. 123, 45678 Testville, Testland";
            Assert.AreEqual(expected, address.GetFullAddress());
        }

        [TestMethod]
        public void PostalAddress_ToString_ShouldReturnFullAddress()
        {
            var address = _builder.AddressedTo("Jane Doe").Build();

            string expected = "Jane Doe\nP.O. 123, 45678 Testville, Testland";
            Assert.AreEqual(expected, address.ToString());
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldBuildValidAddress()
        {
            var address = _builder.Build();

            Assert.AreEqual("P.O.", address.Prefix);
            Assert.AreEqual(123, address.BoxNumber);
            Assert.AreEqual("45678", address.PostalCode);
            Assert.AreEqual("Testville", address.TownOrCity);
            Assert.AreEqual("Testland", address.Country);
            Assert.IsNull(address.AddressTo);
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldSetAddressToCorrectly()
        {
            var address = _builder.AddressedTo("Recipient Name").Build();

            Assert.AreEqual("Recipient Name", address.AddressTo);
        }

        [TestMethod]
        public void PostalAddress_EqualityOperator_ShouldHandleNullValues()
        {
            PostalAddress? address1 = null;
            PostalAddress? address2 = null;

            Assert.IsTrue(address1 == address2);
            Assert.IsFalse(address1 != address2);
        }

        [TestMethod]
        public void PostalAddress_EqualityOperator_ShouldReturnFalseWhenOnlyOneIsNull()
        {
            var address = _builder.Build();
            PostalAddress? nullAddress = null;

            Assert.IsFalse(address == nullAddress);
            Assert.IsTrue(address != nullAddress);
        }

        [TestMethod]
        public void PostalAddress_Builder_ShouldTrimFields()
        {
            var address = _builder
                .WithPrefix(" P.O. ")
                .WithPostalCode(" 45678 ")
                .Build();

            Assert.AreEqual("P.O.", address.Prefix);
            Assert.AreEqual("45678", address.PostalCode);
        }

        [TestMethod]
        public void PostalAddress_Equals_ShouldHandleAddressToDifferences()
        {
            var address1 = _builder.Build();
            var address2 = _builder.AddressedTo("John Doe").Build();

            Assert.IsFalse(address1.Equals(address2));
        }
    }
}
