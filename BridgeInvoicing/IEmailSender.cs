using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing
{
    public interface IEmailSender
    {
        void SendEmail(string toAddress, string subject, string body, string attachmentFileName);
    }

    public interface IFileHelper
    {
        string WriteFile(string text, string filename);
        string GetFile(string filename);
        string WriteFile(byte[] dataArray, string filename);
        void DeleteFile(string filename);
        void ClearFolder(string tempInvoiceFolder);
    }
}
