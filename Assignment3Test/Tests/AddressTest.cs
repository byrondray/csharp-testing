using Assignment3.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressTests
{
    [TestClass]
    public class AddressTest
    {
        private Address address1;
        private Address address2;

        [TestInitialize]
        public void SetUp()
        {
            address1 = new Address("1a", 777, "56th avenue", "v7n2m8", "surrey");
            address2 = new Address(null, 123, "main street", "v7r2g2", "west vancouver");
        }

        [TestCleanup]
        public void TearDown()
        {
            address1 = null;
            address2 = null;
        }

        [TestMethod]
        public void GetUnitNumber()
        {
            Assert.AreEqual("1a", address1.UnitNumber);
            Assert.IsNull(address2.UnitNumber);
        }

        [TestMethod]
        public void GetStreetNumber()
        {
            Assert.AreEqual(777, address1.StreetNumber);
            Assert.AreEqual(123, address2.StreetNumber);
        }

        [TestMethod]
        public void GetStreetName()
        {
            Assert.AreEqual("56th avenue", address1.StreetName);
            Assert.AreEqual("main street", address2.StreetName);
        }

        [TestMethod]
        public void GetPostalCode()
        {
            Assert.AreEqual("v7n2m8", address1.PostalCode);
            Assert.AreEqual("v7r2g2", address2.PostalCode);
        }

        [TestMethod]
        public void GetCity()
        {
            Assert.AreEqual("surrey", address1.City);
            Assert.AreEqual("west vancouver", address2.City);
        }

        [TestMethod]
        public void GetExpectedExceptionsUnitNumber()
        {
            var ex1 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("", 777, "56th avenue", "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid unit number: ", ex1.Message);

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("111111", 777, "56th avenue", "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid unit number: 111111", ex2.Message);
        }



        [TestMethod]
        public void GetExpectedExceptionsStreetNumber()
        {
            var ex1 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", -1, "56th avenue", "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid street number: -1", ex1.Message);

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 1000000, "56th avenue", "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid street number: 1000000", ex2.Message);
        }

        [TestMethod]
        public void GetExpectedExceptionsStreetName()
        {
            var ex1 = Assert.ThrowsException<ArgumentNullException>(() =>
                address1 = new Address("1a", 777, null, "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid street name: null (Parameter 'streetName')", ex1.Message);

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 777, "", "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid street name: ", ex2.Message);

            var ex3 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 777, "abcdefghijklmnopqrstu", "v7n2m8", "surrey"));
            Assert.AreEqual("Invalid street name: abcdefghijklmnopqrstu", ex3.Message);
        }


        [TestMethod]
        public void GetExpectedExceptionsPostalCode()
        {
            var ex1 = Assert.ThrowsException<ArgumentNullException>(() =>
                address1 = new Address("1a", 777, "56th avenue", null, "surrey"));
            Assert.AreEqual("Invalid postal code: null (Parameter 'postalCode')", ex1.Message);

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 777, "56th avenue", "1234", "surrey"));
            Assert.AreEqual("Invalid postal code: 1234", ex2.Message);

            var ex3 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 777, "56th avenue", "1234567", "surrey"));
            Assert.AreEqual("Invalid postal code: 1234567", ex3.Message);
        }

        [TestMethod]
        public void GetExpectedExceptionsCity()
        {
            var ex1 = Assert.ThrowsException<ArgumentNullException>(() =>
                address1 = new Address("1a", 777, "56th avenue", "v7n2m8", null));
            Assert.AreEqual("Invalid city: null (Parameter 'city')", ex1.Message);

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 777, "56th avenue", "v7n2m8", ""));
            Assert.AreEqual("Invalid city: ", ex2.Message);

            var ex3 = Assert.ThrowsException<ArgumentException>(() =>
                address1 = new Address("1a", 777, "56th avenue", "v7n2m8", "0123456789012345678901234567890"));
            Assert.AreEqual("Invalid city: 0123456789012345678901234567890", ex3.Message);
        }

    }
}
