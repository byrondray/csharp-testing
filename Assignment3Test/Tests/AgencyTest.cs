using Assignment3.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AgencyTests
{
    [TestClass]
    public class AgencyTest
    {
        private Agency agency;

        private static Address a1 = new Address("1a", 777, "56th avenue", "v7n2m8", "surrey");
        private Property p1 = new Property(499000.00, a1, 2, false, "residence", "abc123");

        private static Address a2 = new Address(null, 123, "main street", "v7r2g2", "west vancouver");
        private Property p2 = new Property(5999999.00, a2, 5, true, "residence", "xyz789");

        private static Address a3 = new Address(null, 456, "elm street", "90210", "los angeles");
        private Property p3 = new Property(2500000.00, a3, 6, true, "residence", "777def");

        private static Address a4 = new Address("44", 1111, "maple street", "v8y3r5", "vancouver");
        private Property p4 = new Property(1000000.00, a4, 1, false, "retail", "876tru");

        private static Address a5 = new Address("9", 99, "gretzky way", "t6v7h3", "toronto");
        private Property p5 = new Property(99999.00, a5, 1, false, "commercial", "9999");

        private static Address a6 = new Address("b", 711, "country road", "v8h5f5", "maple ridge");
        private Property p6 = new Property(740100.00, a6, 3, false, "residence", "mr6789");

        private static Address a7 = new Address(null, 8785, "pinnacle avenue", "v9u3h3", "north vancouver");
        private Property p7 = new Property(15000000.00, a7, 20, true, "residence", "78444a");

        private static Address a8 = new Address(null, 800, "elm street", "90557", "los angeles");
        private Property p8 = new Property(7100000.00, a8, 10, false, "residence", "mmm33");

        private static Address a9 = new Address(null, 1515, "main street", "v8y7r3", "west vancouver");
        private Property p9 = new Property(4000000.00, a9, 2, true, "commercial", "678T");

        private static Address a10 = new Address("6", 60, "60th street", "v8u9b1", "burnaby");
        private Property p10 = new Property(700000.00, a10, 2, true, "retail", "y6yyy");

        private static Address a11 = new Address("7h", 1500, "railway avenue", "v9v5v4", "richmond");
        private Property p11 = new Property(840000.00, a11, 4, false, "commercial", "A1212");

        private static Address a12 = new Address(null, 333, "elm street", "90111", "los angeles");
        private Property p12 = new Property(1600000.00, a12, 3, false, "residence", "9000a");

        [TestInitialize]
        public void SetUp()
        {
            agency = new Agency("BCIT Ltd");
            agency.AddProperty(p1);
            agency.AddProperty(p2);
            agency.AddProperty(p3);
            agency.AddProperty(p4);
            agency.AddProperty(p5);
            agency.AddProperty(p6);
            agency.AddProperty(p7);
            agency.AddProperty(p8);
            agency.AddProperty(p9);
            agency.AddProperty(p10);
            agency.AddProperty(p11);
            agency.AddProperty(p12);
        }

        [TestCleanup]
        public void TearDown()
        {
            agency = null;
        }

        [TestMethod]
        public void AddGetProperty()
        {
            Assert.IsNull(agency.GetProperty("x"));

            var a13 = new Address(null, 333, "elm street", "90111", "los angeles");
            var p13 = new Property(1600000.00, a13, 3, false, "residence", "x");

            agency.AddProperty(p13);

            Assert.AreSame(p13, agency.GetProperty("x"));
        }

        [TestMethod]
        public void RemoveProperty()
        {
            Assert.IsNull(agency.GetProperty("x"));

            var a13 = new Address(null, 333, "elm street", "90111", "los angeles");
            var p13 = new Property(1600000.00, a13, 3, false, "residence", "x");

            agency.AddProperty(p13);
            Assert.AreSame(p13, agency.GetProperty("x"));

            agency.RemoveProperty("x");
            Assert.IsNull(agency.GetProperty("x"));
        }

        [TestMethod]
        public void GetTotalPropertyValues()
        {
            Assert.AreEqual(40079098, agency.GetTotalPropertyValues());
        }

        [TestMethod]
        public void GetPropertiesWithPools()
        {
            var expected = new List<Property> { p2, p3, p7, p9, p10 };
            var result = agency.GetPropertiesWithPools();

            Assert.AreEqual(expected.Count, result.Count);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void GetPropertiesBetween()
        {
            var matches = new[] { p3, p4, p6, p11, p12 };
            var result = agency.GetPropertiesBetween(700001, 2500000);

            Assert.AreEqual(matches.Length, result.Length);
            CollectionAssert.AreEquivalent(matches, result);
        }

        [TestMethod]
        public void GetPropertiesOn()
        {
            var expected = new List<Address> { p3.Address, p8.Address, p12.Address };
            var result = agency.GetPropertiesOn("elm street");

            Assert.AreEqual(expected.Count, result.Count);
            CollectionAssert.AreEquivalent(expected, result);

            Assert.IsNull(agency.GetPropertiesOn("fake street"));
        }

        [TestMethod]
        public void GetPropertiesWithBedrooms()
        {
            var expected = new Dictionary<string, Property>
            {
                { p2.PropertyId, p2 },
                { p3.PropertyId, p3 },
                { p8.PropertyId, p8 },
                { p11.PropertyId, p11 }
            };

            var result = agency.GetPropertiesWithBedrooms(4, 12);

            Assert.AreEqual(expected.Count, result.Count);
            CollectionAssert.AreEquivalent(expected.Keys.ToList(), result.Keys.ToList());

            Assert.IsNull(agency.GetPropertiesWithBedrooms(7, 9));
        }

        [TestMethod]
        public void GetPropertiesOfType()
        {
            var result = agency.GetPropertiesOfType("commercial");
            var expectedStrings = new[]
            {
                "Type: COMMERCIAL\n",
                ") Property 9999: unit #9 at 99 Gretzky Way T6V7H3 in Toronto (1 bedroom): $99999.\n",
                ") Property 678T: 1515 Main Street V8Y7R3 in West Vancouver (2 bedrooms plus pool): $4000000.\n",
                ") Property A1212: unit #7h at 1500 Railway Avenue V9V5V4 in Richmond (4 bedrooms): $840000.\n"
            };

            Assert.AreEqual(expectedStrings.Length, result.Count);
            foreach (var str in expectedStrings)
            {
                Assert.IsTrue(result.Any(r => r.Contains(str)));
            }
        }
    }
}
