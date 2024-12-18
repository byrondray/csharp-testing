using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment3.Models;

namespace PropertyTests
{
    [TestClass]
    public class PropertyTest
    {
        private Property property1;
        private Property property2;
        private Property property3;
        private Address address;

        [TestInitialize]
        public void SetUp()
        {
            address = new Address("1a", 777, "56th avenue", "v7n2m8", "surrey");

            property1 = new Property(499000, address, 1, true, "resiDEnce", "11111");
            property2 = new Property(1000000, address, 2, false, "commErcial", "22222");
            property3 = new Property(840000, address, 3, false, "rETAil", "xxxxx");
        }

        [TestCleanup]
        public void TearDown()
        {
            address = null;

            property1 = null;
            property2 = null;
            property3 = null;
        }

        [TestMethod]
        public void GetPriceUsd()
        {
            Assert.AreEqual(499000, property1.PriceUsd);
            Assert.AreEqual(1000000, property2.PriceUsd);
            Assert.AreEqual(840000, property3.PriceUsd);
        }

        [TestMethod]
        public void GetAddress()
        {
            Assert.AreEqual(address, property1.Address);
        }

        [TestMethod]
        public void GetNumberOfBedrooms()
        {
            Assert.AreEqual(1, property1.NumberOfBedrooms);
            Assert.AreEqual(2, property2.NumberOfBedrooms);
            Assert.AreEqual(3, property3.NumberOfBedrooms);
        }

        [TestMethod]
        public void HasSwimmingPool()
        {
            Assert.IsTrue(property1.HasSwimmingPool);
            Assert.IsFalse(property2.HasSwimmingPool);
            Assert.IsFalse(property3.HasSwimmingPool);
        }

        [TestMethod]
        public void GetType()
        {
            Assert.AreEqual("resiDEnce", property1.Type);
            Assert.AreEqual("commErcial", property2.Type);
            Assert.AreEqual("rETAil", property3.Type);
        }

        [TestMethod]
        public void GetPropertyId()
        {
            Assert.AreEqual("11111", property1.PropertyId);
            Assert.AreEqual("22222", property2.PropertyId);
            Assert.AreEqual("xxxxx", property3.PropertyId);
        }

        [TestMethod]
        public void SetPriceUsd()
        {
            property1.PriceUsd = 777123;
            Assert.AreEqual(777123, property1.PriceUsd);
        }

        [TestMethod]
        public void GetExpectedExceptionsPriceUsd()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() =>
            {
                property1 = new Property(-0.01, address, 1, true, "residence", "11111");
            });
            Assert.AreEqual("Invalid price: -0.01", ex.Message);
        }

        [TestMethod]
        public void GetExpectedExceptionsAddress()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                property1 = new Property(499000, null, 1, true, "residence", "11111");
            });
            Assert.AreEqual("Invalid address: null (Parameter 'address')", ex.Message);
        }


        [TestMethod]
        public void GetExpectedExceptionsNumBedrooms()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() =>
            {
                property1 = new Property(499000, address, 0, true, "residence", "11111");
            });
            Assert.AreEqual("Invalid number of bedrooms: 0", ex.Message);

            ex = Assert.ThrowsException<ArgumentException>(() =>
            {
                property1 = new Property(499000, address, 21, true, "residence", "11111");
            });
            Assert.AreEqual("Invalid number of bedrooms: 21", ex.Message);
        }

        [TestMethod]
        public void GetExpectedExceptionsPropertyType()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                property1 = new Property(499000, address, 1, true, null, "11111");
            });
            StringAssert.Contains(ex.Message, "Invalid property type: null");

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
            {
                property1 = new Property(499000, address, 2, true, "residencee", "11111");
            });
            Assert.AreEqual("Invalid property type: residencee", ex2.Message);
        }


        [TestMethod]
        public void GetExpectedExceptionsPropertyId()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                property1 = new Property(499000, address, 1, true, "residence", null);
            });
            Assert.AreEqual("Invalid property id: null (Parameter 'propertyId')", ex.Message);

            var ex2 = Assert.ThrowsException<ArgumentException>(() =>
            {
                property1 = new Property(499000, address, 1, true, "residence", "");
            });
            Assert.AreEqual("Invalid property id: ", ex2.Message);

            ex2 = Assert.ThrowsException<ArgumentException>(() =>
            {
                property1 = new Property(499000, address, 1, true, "residence", "1234567");
            });
            Assert.AreEqual("Invalid property id: 1234567", ex2.Message);
        }


    }
}
