using BridgeInvoicing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Emails
{
    public class Invoice
    {
        private string _studentName;
        private string _toEmail;
        private List<Session> _sessions;
        private decimal? _total;
        private string _template;
        private string _invoiceNumber;

        public string InvoiceNumber { get { return _invoiceNumber; } }

        public Invoice(string invoiceNumber = null)
        {
            _invoiceNumber = invoiceNumber;
        }

        public Invoice To(Student student)
        {
            this._toEmail = student.Email;
            this._studentName = student.Name;
            return this;
        }

        public Invoice Sessions(List<Session> sessions)
        {
            this._sessions = sessions;
            this._total = sessions.Sum(x => x.Price);
            return this;
        }

        public Invoice LoadTemplate(string template)
        {
            this._template = template;
            return this;
        }

        public string BuildHtml()
        {
            var itemsStart = _template.IndexOf("<items>");
            var itemsEnd = _template.IndexOf("</items>") + 8;
            var date = DateTime.Now.ToString("d MMM yyyy");
            var htmlHead = _template.Substring(0, itemsStart - 1)
                .Replace(InvoiceTemplate.recipientName, _studentName)
                .Replace(InvoiceTemplate.invoiceNr, _invoiceNumber)
                .Replace(InvoiceTemplate.invoiceDate, date)
                .Replace(InvoiceTemplate.amountDue, (_total ?? 0).ToString("#.00"));
            var itemsTemplate = _template.Substring(itemsStart, itemsEnd - itemsStart);
            StringBuilder sb = new StringBuilder();
            foreach (var item in _sessions)
            {
                var studentName = item.Student == null ? _studentName : item.Student.Name;
                sb.Append(itemsTemplate.Replace("items", "tr")
                    .Replace(InvoiceTemplate.Item.date, item.Date.ToString("d MMM yyyy HH:mm"))
                    .Replace(InvoiceTemplate.Item.student, studentName)
                    .Replace(InvoiceTemplate.Item.horse, item.Horse)
                    .Replace(InvoiceTemplate.Item.rate, (item.Price ?? 0).ToString("#.00")));
            }

            var htmlFoot = _template.Substring(itemsEnd)
                .Replace(InvoiceTemplate.amountDue, (_total ?? 0).ToString("#.00"));

            return htmlHead + sb.ToString() + htmlFoot;
        }
        public string GetHtmlEmail()
        {
            StringBuilder invoicelist = new StringBuilder();
            invoicelist.AppendFormat(@"<html>
    <body>
        <p>Dear {0},</p>", _studentName);
            invoicelist.Append("<p>Please take note of the following sessions that needs paying</p>");

            invoicelist.Append(@"<table>
    <thead>
        <tr>
            <th>Date</th>
            <th>Horse</th>
            <th>Price</th>
            <th>Comment</th>
        </tr>
    </thead>
    <tbody>");

            foreach (var item in _sessions)
            {
                invoicelist.AppendFormat(@"<tr>
    <td>{0}</td>
    <td>{1}</td>
    <td>{2}</td>
    <td>{3}</td>
</tr>", item.Date, item.Horse, item.Price, item.Comment);
            }
            invoicelist.Append("</tbody></table>");

            invoicelist.Append("<p>Thank you!</p></body></html>");
            return invoicelist.ToString();
        }

        public string GetPlaintextEmail()
        {
            string start = string.Format(@"Dear {0},\n
\n
Date\tHorse\tPrice\Comment\n", _studentName);
            StringBuilder sb = new StringBuilder(start);

            foreach (var item in _sessions)
            {
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\n", item.Date, item.Horse, item.Price, item.Comment);
            }

            sb.AppendLine("Thanks");
            return sb.ToString();
        }

        internal static class InvoiceTemplate
        {
            internal const string recipientName = "@recipientName";
            internal const string invoiceNr = "@invoiceNr";
            internal const string invoiceDate = "@invoiceDate";
            internal const string amountDue = "@amountDue";

            internal static class Item
            {
                internal const string date = "@date";
                internal const string student = "@student";
                internal const string horse = "@horse";
                internal const string rate = "@rate";

            }
        }
    }
}
