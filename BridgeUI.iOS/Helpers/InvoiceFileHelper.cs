using BridgeInvoicing.Helpers;
using BridgeUI.iOS.Helpers;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(InvoiceFileHelper))]
namespace BridgeUI.iOS.Helpers
{
    public class InvoiceFileHelper : IInvoiceFileHelper
    {
        public void ClearInvoiceAttachmentFolder(string tempInvoiceFolder)
        {
            var documents = AppFileDirectories.MyDocuments;
            var tmpInvoices= Path.Combine(documents, "..", AppFileDirectories.Temp, AppFileDirectories.Invoices);

            if (Directory.Exists(tmpInvoices))
            {
                var files = Directory.EnumerateFiles(tmpInvoices);
                foreach (var fle in files)
                {
                    File.Delete(fle);
                }
            }
        }

        public string CreateTempInvoiceAttachment(string filename, string content)
        {
            var documents = AppFileDirectories.MyDocuments;
            var tmpInvoices = Path.Combine(documents, "..", AppFileDirectories.Temp, AppFileDirectories.Invoices);
            var fullfilepath = System.IO.Path.Combine(tmpInvoices, filename);

            File.WriteAllText(fullfilepath, content);
            return fullfilepath;
        }

        public bool InvoiceTemplateFileExists(string filename)
        {
            return File.Exists(GetFullTemplatePath(filename)));
        }

        public string GetTemplateFile(string filename)
        {
            var fullFilePath = GetFullTemplatePath(filename);
            if (File.Exists(fullFilePath))
            {
                return File.ReadAllText(fullFilePath);
            }
            return null;
        }

        public string WriteTemplateFile(byte[] dataArray, string filename)
        {
            System.Diagnostics.Debug.WriteLine("Trying to write file");
            string fullFilename = GetFullTemplatePath(filename);
            System.Diagnostics.Debug.WriteLine(fullFilename);

            File.WriteAllBytes(fullFilename, dataArray);
            return fullFilename;
        }

        private string GetFullTemplatePath(string filename)
        {
            var documents = AppFileDirectories.MyDocuments;
            var libraryInvTemplate = Path.Combine(documents, "..", AppFileDirectories.Library, AppFileDirectories.InvoiceTemplate);
            return Path.Combine(libraryInvTemplate, filename);
        }
    }
}
