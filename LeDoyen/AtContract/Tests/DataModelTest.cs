using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using LeDoyen.AtContract.Business.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeDoyen.AtContract.Tests
{
    ///<summary>
    ///  This is a test class for ConfigTest and is intended
    ///  to contain all ConfigTest Unit Tests
    ///</summary>
    [TestClass]
    public class DataModelTest : BaseTests
    {
        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        ///<summary>
        ///  A test for Load
        ///</summary>
        [TestMethod]
        public void LoadTest()
        {
            var ent = new LeDoyenEntities();

            var ctr = Contractor.CreateContractor(int.MinValue, "Вася", "Илларионович", "Пупькинд", "Wassily Poupkind",
                                                  "living home", "жылище", "CO123456", "issued by",
                                                  new DateTime(1999, 12, 1));
            PaymentType pay = (PaymentType) ent.GetObjectByKey(new EntityKey("LeDoyenEntities.PaymentTypes", "ID", 1));
            Project proj = new Project
                               {
                                   NameEn = "halk",
                                   NameUk = "Халк",
                                   TypeNameEn = "moovie",
                                   TypeNameUk = "фільм",
                                   WorkNameEn = "halker",
                                   WorkNameUk = "xxx234"
                               };
            var agr = new Agreement
                          {
                              AgreementDate = new DateTime(2010, 4, 16),
                              AgreementNumber = 3,
                              Contractor = ctr,
                              PaymentType = pay,
                              Project = proj,
                              PaymentDueDate = new DateTime(2010, 5, 21),
                              ServiceStartDate = new DateTime(2010, 4, 21),
                              ServiceEndDate = new DateTime(2010, 4, 26),
                          };
            ent.AddToAgreements(agr);
            ent.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            Assert.IsNotNull(ctr.ID);
            Assert.IsNotNull(agr.ID);
            Assert.IsNotNull(proj.ID);
        }

        [TestMethod]
        public void CheckTest()
        {
            using (var ent = new LeDoyenEntities())
            {
                Project p = (from project in ent.Projects 
                             where project.ID == 1
                             select project).FirstOrDefault();
                Assert.IsTrue(p.Agreements.Count > 0);
            }
        }
    }
}