using BridgeInvoicing.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BridgeInvoicing.Tests
{
    [TestClass]
    public class InvoiceGenerationTests
    {
        IDbFileHelper fileHelper = new TestsDbFileHelper();
        private const string INVOICE_TEMPLATE_FILE = "invoice.html";

        [TestMethod]
        public void Test()
        {
            var student = new Domain.Student();
            student.Email = "email@email.com";
            student.Name = "Good Student";
            student.Phone = "0123456789";
            var list = new List<Domain.Session>();
            list.Add(new Domain.Session() { Date = DateTime.Now, Horse = "Horse Name", Price = 350, Comment = "Comment comment comment comment" });
            list.Add(new Domain.Session() { Date = DateTime.Now, Horse = "Horse Name1", Price = 351, Comment = "Comment comment comment comment" });
            list.Add(new Domain.Session() { Date = DateTime.Now, Horse = "Horse Name2", Price = 352, Comment = "Comment comment comment comment" });
            list.ForEach(x => x.SetStudent(student));

            var invoiceEmail = new Emails.Invoice("Inv001");
            var template = System.IO.File.ReadAllText(INVOICE_TEMPLATE_FILE);

            invoiceEmail
                    .To(student)
                    .Sessions(list)
                    .LoadTemplate(template);
            var result = invoiceEmail.BuildHtml();

            System.IO.File.WriteAllText("result-" + INVOICE_TEMPLATE_FILE, result);

        }
    }
}
