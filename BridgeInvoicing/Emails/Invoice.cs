using BridgeInvoicing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Emails
{
    internal class Invoice
    {
        private string _studentName;
        private string _toEmail;
        private List<Session> _sessions;

        public Invoice()
        { }

        public Invoice To(Student student)
        {
            this._toEmail = student.Email;
            this._studentName = student.Name;
            return this;
        }

        public Invoice Sessions(List<Session> sessions)
        {
            this._sessions = sessions;
            return this;
        }

        public string GetEmail()
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
    }
}
