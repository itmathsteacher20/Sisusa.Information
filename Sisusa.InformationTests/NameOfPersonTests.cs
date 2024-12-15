using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sisusa.People;
using System;
using Sisusa.Information.Identification;

namespace Sisusa.People.Tests
{
    [TestClass]
    public class NameOfPersonTests
    {
        [TestMethod]
        public void TestEquals_ShouldReturnTrue_ForIdenticalNames()
        {
            var person1 = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();
            var person2 = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();

            Assert.IsTrue(person1.Equals(person2));
        }

        [TestMethod]
        public void TestEquals_ShouldReturnFalse_ForDifferentNames()
        {
            var person1 = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();
            var person2 = NameOfPerson.New().WithFirstName("Jane").AndLastName("Doe").TryBuild();

            Assert.IsFalse(person1.Equals(person2));
        }

        [TestMethod]
        public void TestGetFullname_ShouldReturnFullname_WithSurnameFirst()
        {
            var person = NameOfPerson.New().WithFirstName("John").HavingOtherNames("Paul").AndLastName("Doe").TryBuild();
            var fullName = person.GetFullname(NameOrder.SurnameNames);

            Assert.AreEqual("Doe John Paul", fullName);
        }

        [TestMethod]
        public void TestGetFullname_ShouldReturnFullname_WithFirstNameFirst()
        {
            var person = NameOfPerson.New().WithFirstName("John").HavingOtherNames("Paul").AndLastName("Doe").TryBuild();
            var fullName = person.GetFullname(NameOrder.NamesSurname);

            Assert.AreEqual("John Paul Doe", fullName);
        }

        [TestMethod]
        public void TestGetInitials_ShouldReturnInitials()
        {
            var person = NameOfPerson.New().WithFirstName("John").HavingOtherNames("Paul").AndLastName("Doe").TryBuild();
            var initials = person.GetInitials(NameOrder.SurnameNames);

            Assert.AreEqual("D.J.P.", initials);
        }

        [TestMethod]
        public void TestGetInitials_ShouldReturnInitialsWithFirstNameFirst()
        {
            var person = NameOfPerson.New().WithFirstName("John").HavingOtherNames("Paul").AndLastName("Doe").TryBuild();
            var initials = person.GetInitials(NameOrder.NamesSurname);

            Assert.AreEqual("J.P.D.", initials);
        }

        [TestMethod]
        public void TestGetShortenedName_ShouldReturnShortenedName()
        {
            var person = NameOfPerson.New().WithFirstName("John").HavingOtherNames("Paul").AndLastName("Doe").TryBuild();
            var shortenedName = person.GetShortenedName(NameOrder.SurnameNames);

            Assert.AreEqual("Doe J.P.", shortenedName);
        }

        [TestMethod]
        public void TestEquals_ShouldReturnFalse_ForNullObject()
        {
            var person = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();

            Assert.IsFalse(person.Equals(null));
        }

        [TestMethod]
        public void TestGetHashCode_ShouldReturnSameHashCode_ForEqualObjects()
        {
            var person1 = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();
            var person2 = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();

            Assert.AreEqual(person1.GetHashCode(), person2.GetHashCode());
        }

        [TestMethod]
        public void TestConstructor_ShouldThrowException_WhenInvalidName()
        {
            try
            {
                var person = NameOfPerson.New().WithFirstName("").AndLastName("").TryBuild();
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (InvalidNameOfPersonException ex)
            {
                Assert.AreEqual("Name of person must at least have a valid firstname and surname.", ex.Message);
            }
        }

        [TestMethod]
        public void TestHasOtherNames_ShouldReturnTrue_WhenMiddleNamesPresent()
        {
            var person = NameOfPerson.New().WithFirstName("John").HavingOtherNames("Paul").AndLastName("Doe").TryBuild();

            Assert.IsTrue(person.HasOtherNames);
        }

        [TestMethod]
        public void TestHasOtherNames_ShouldReturnFalse_WhenNoMiddleNames()
        {
            var person = NameOfPerson.New().WithFirstName("John").AndLastName("Doe").TryBuild();

            Assert.IsFalse(person.HasOtherNames);
        }
    }
}