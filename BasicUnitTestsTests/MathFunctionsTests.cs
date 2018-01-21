using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicUnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using BasicUnitTests.Fakes;

namespace BasicUnitTests.Tests
{
    [TestClass()]
    public class MathFunctionsTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [DataSource(@"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TR24_TestData;Data Source=ARNONGX1YOGA\SQLEXPRESS", "AddNumbers_Data")]
        [TestMethod()]
        public void AddNumbersTest()
        {
            var unitUnderTest = new BasicUnitTests.MathFunctions();
            var a = Convert.ToInt32(TestContext.DataRow["a"]);
            var b = Convert.ToInt32(TestContext.DataRow["b"]);

            var result = unitUnderTest.AddNumbers(a, b);
            Assert.AreEqual(result, Convert.ToInt32(TestContext.DataRow["sum"]));
        }

        [TestMethod()]
        public void GetFileAggregationTest()
        {
            using (ShimsContext.Create())
            {
                ShimFileAggregator.AllInstances.GetNumbersFromFileString = (@this,String) =>
                {
                    return new List<int>() { 4, 5, 6 };
                };

                var unitUnderTest = new FileAggregator();
                var result = unitUnderTest.GetFileAggregation("FakeFile.csv");
                Assert.AreEqual(15, result);
            }
        }
    }
}