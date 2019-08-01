using Android.Content;
using BridgeInvoicing.Helpers;
using BridgeUI.Driod.Helpers;
using System.IO;
using Xamarin.Forms;
using env = System.Environment;

[assembly: Dependency(typeof(InvoiceFileHelper))]
namespace BridgeUI.Driod.Helpers
{
    public class InvoiceFileHelper : IInvoiceFileHelper
    {
        Context context = Android.App.Application.Context;
        public string GetTemplateFile(string filename)
        {
            string fullFilename = GetFullFilePath(filename);
            if (File.Exists(fullFilename))
            {
                return File.ReadAllText(fullFilename);
            }
            return null;
        }

        private static string GetFullFilePath(string filename)
        {
            string path = env.GetFolderPath(env.SpecialFolder.Personal);
            string fullFilename = Path.Combine(path, filename);
            return fullFilename;
        }

        public string WriteTemplateFile(byte[] dataArray, string filename)
        {
            System.Diagnostics.Debug.WriteLine("Trying to write file");
            string fullFilename = GetFullFilePath(filename);
            System.Diagnostics.Debug.WriteLine(fullFilename);

            File.WriteAllBytes(fullFilename, dataArray);
            return fullFilename;
        }

        public void ClearInvoiceAttachmentFolder(string folder)
        {
            if (Directory.Exists(folder))
            {
                var files = Directory.EnumerateFiles(folder);
                foreach (var fle in files)
                {
                    File.Delete(fle);
                }
            }
        }

        public bool InvoiceTemplateFileExists(string filename)
        {
            string fullFilename = GetFullFilePath(filename);
            return File.Exists(fullFilename);
        }

        public string CreateTempInvoiceAttachment(string filename, string content)
        {
            var fullfilepath = System.IO.Path.Combine(context.CacheDir.Path, filename);
            File.WriteAllText(fullfilepath, content);
            return fullfilepath;
        }
    }
}