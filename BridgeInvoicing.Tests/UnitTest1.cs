using BridgeInvoicing.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BridgeInvoicing.Tests
{
    [TestClass]
    public class UnitTest1
    {
        IDbFileHelper fileHelper = new TestsDbFileHelper();

        [TestMethod]
        public void CanCreateDatabase()
        {
            var db = new BridgeInvoicing.Data.Database(fileHelper.GetLocalFilePath("BI_SqlLite.db3"));
            Assert.IsNotNull(db);
        }

        [TestMethod]
        public async Task InsertAndSearchStudent()
        {
            var db = new BridgeInvoicing.Data.Database(fileHelper.GetLocalFilePath("BI_SqlLite.db3"));
            //var student = new Student();
            //student.Name = "Magi";
            //student.Email = "magi@gmail.com";
            //student.Phone = "0123456789";
            //var result = await db.SaveStudent(student);
            //Assert.AreEqual(1, result);
            //Assert.IsTrue(student.Id > 0, "Studnt not saved properly?");


            var searchResult = await db.SearchStudent("Mag");
            Assert.IsTrue(searchResult.Count > 0, "Magi not found");
            var amgi = searchResult[0];
            //Assert.AreEqual(student.Name, amgi.Name);
        }

        [TestMethod]
        public async Task InsertSessionsAndSearchHorse()
        {
            var db = new BridgeInvoicing.Data.Database(fileHelper.GetLocalFilePath("BI_SqlLite.db3"));
            //Session session = new Session();
            //var student = new Student();
            //student.Id = 1;
            //student.Name = "Magi";
            //student.Email = "magi@gmail.com";
            //student.Phone = "0123456789";
            //session.SetStudent(student);

            //session.Comment = "Random comment";
            //session.Date = DateTime.Now;
            //session.Horse = "CMSG Horse";
            //session.Price = 400.00M;
            //var result = await db.AddSession(session);
            //Assert.AreEqual(1, result);
            //Assert.IsTrue(session.Id > 0);

            var horses = await db.SearchHorseName("CM");
            Assert.IsTrue(horses.Count > 0, "Horses not found");
        }

        [TestMethod]
        public async Task GetAllSessions() {
            var db = new BridgeInvoicing.Data.Database(fileHelper.GetLocalFilePath("BI_SqlLite.db3"));
            var result = await db.GetAllSessions(new DateTime(2017, 04, 01), DateTime.Now, null);

        }
    }
}
