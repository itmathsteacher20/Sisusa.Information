using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.People;
using System;
using Sisusa.Information.Communication;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class DetailedAddressTests
    {
        // Test if AddressBuilder correctly constructs a DetailedAddress
        [TestMethod]
        public void TestAddressBuilder_CorrectlyBuildsAddress()
        {
            // Arrange
            var expectedCountry = "Eswatini";
            var expectedCity = "Mbabane";
            var expectedStreet = "Gwamile Street";
            var expectedPostalCode = "H100";
            var expectedGeographicalMarker = "Near the market";

            // Act
            var address = new DetailedAddress.AddressBuilder()
                .FromCountry(expectedCountry)
                .InTownOrCity(expectedCity)
                .InStreetOrHouse(expectedStreet)
                .HavingZipCode(expectedPostalCode)
                .WithNearestGeographicalMarker(expectedGeographicalMarker)
                .Create();

            // Assert
            Assert.AreEqual(expectedCountry, address.Country);
            Assert.AreEqual(expectedCity, address.City);
            Assert.AreEqual(expectedStreet, address.StreetName);
            Assert.AreEqual(expectedPostalCode, address.PostalCode);
            Assert.AreEqual(expectedGeographicalMarker, address.NearestGeographicalMarker);
        }

        // Test invalid country in AddressBuilder
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddressBuilder_FromCountry_ThrowsArgumentNullException_WhenInvalidCountry()
        {
            var address = new DetailedAddress.AddressBuilder()
                .FromCountry(""); // Invalid empty country
        }

        // Test invalid city in AddressBuilder
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddressBuilder_InTownOrCity_ThrowsArgumentNullException_WhenInvalidCity()
        {
            var address = new DetailedAddress.AddressBuilder()
                .InTownOrCity(""); // Invalid empty city
        }

        // Test equals method (same address)
        [TestMethod]
        public void TestEquals_ReturnsTrue_WhenAddressesAreEqual()
        {
            var address1 = new DetailedAddress.AddressBuilder()
                .FromCountry("Eswatini")
                .InTownOrCity("Mbabane")
                .Create();

            var address2 = new DetailedAddress.AddressBuilder()
                .FromCountry("Eswatini")
                .InTownOrCity("Mbabane")
                .Create();

            Assert.IsTrue(address1.Equals(address2));
        }

        // Test equals method (different address)
        [TestMethod]
        public void TestEquals_ReturnsFalse_WhenAddressesAreDifferent()
        {
            var address1 = new DetailedAddress.AddressBuilder()
                .FromCountry("Eswatini")
                .InTownOrCity("Mbabane")
                .Create();

            var address2 = new DetailedAddress.AddressBuilder()
                .FromCountry("South Africa")
                .InTownOrCity("Pretoria")
                .Create();

            Assert.IsFalse(address1.Equals(address2));
        }

        // Test GetHashCode consistency
        [TestMethod]
        public void TestGetHashCode_ReturnsConsistentHashCode()
        {
            var address = new DetailedAddress.AddressBuilder()
                .FromCountry("Eswatini")
                .InTownOrCity("Mbabane")
                .Create();

            var hashCode1 = address.GetHashCode();
            var hashCode2 = address.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2);
        }

        // Test if AddressBuilder allows empty values for optional properties like PostalCode and StreetName
        [TestMethod]
        public void TestAddressBuilder_OptionalProperties_DefaultValues()
        {
            var address = new DetailedAddress.AddressBuilder()
                .FromCountry("Eswatini")
                .InTownOrCity("Mbabane")
                .Create();

            Assert.AreEqual(string.Empty, address.StreetName);
            Assert.AreEqual(string.Empty, address.PostalCode);
        }
    }
}

