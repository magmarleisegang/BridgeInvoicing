using BridgeInvoicing;
using BridgeUI.Driod;
using System.IO;
using Xamarin.Forms;
using env = System.Environment;
using System;

[assembly: Dependency(typeof(FileHelper))]
namespace BridgeUI.Driod
{
    public class FileHelper : IFileHelper
    {
        public string GetFile(string filename)
        {
            string fullFilename = GetFullFilePath(filename);

            using (var streamReader = new StreamReader(fullFilename))
            {
                return streamReader.ReadToEnd();
            }
        }

        public string WriteFile(string text, string filename)
        {
            string fullFilename = GetFullFilePath(filename);

            using (var streamWriter = new StreamWriter(fullFilename, true))
            {
                streamWriter.Write(text);
            }

            return fullFilename;
        }

        private static string GetFullFilePath(string filename)
        {
            string path = env.GetFolderPath(env.SpecialFolder.Personal);
            string fullFilename = Path.Combine(path, filename);
            return fullFilename;
        }
    }
}