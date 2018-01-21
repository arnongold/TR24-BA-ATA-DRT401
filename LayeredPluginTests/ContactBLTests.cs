using Microsoft.VisualStudio.TestTools.UnitTesting;
using LayeredPlugin.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.Xrm.Sdk;

namespace LayeredPluginTests.Tests
{
    [TestClass()]
    public class ContactBLTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        [DataSource(@"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TR24_TestData;Data Source=ARNONGX1YOGA\SQLEXPRESS", "ContactBL_TestData")]
        [TestMethod()]
        public void ValidateContactCreation_Success()
        {
            using (ShimsContext.Create())
            {
                var dummyContact = new LayeredPlugin.Model.Fakes.ShimContact
                {
                    AccountIdGet = () => { return new EntityReference("Account", Guid.NewGuid()); }
                };

                LayeredPlugin.DataAccess.Fakes.ShimContactDAL.ConstructorIOrganizationService = (@this,IOrganizationService) => { };
                LayeredPlugin.DataAccess.Fakes.ShimContactDAL.AllInstances.GetNumberOfContactsInAccountGuid = (@this, Guid) =>
                {
                    return Convert.ToInt32(TestContext.DataRow["NumberOfContacts"]);
                };

                var unitUnderTest = new ContactBL(null);
                var result = unitUnderTest.ValidateContactCreation(dummyContact);

                Assert.AreEqual(Convert.ToBoolean(TestContext.DataRow["ExpectedResult"]), result.HasError);
            }
        }
    }
}