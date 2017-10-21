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
            if (File.Exists(fullFilename))
            {
                return File.ReadAllText(fullFilename);
            }
            return null;
        }

        public string WriteFile(string text, string filename)
        {
            string fullFilename = GetFullFilePath(filename);
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilename));
            File.WriteAllText(fullFilename, text);

            return fullFilename;
        }

        private static string GetFullFilePath(string filename)
        {
            string path = env.GetFolderPath(env.SpecialFolder.Personal);
            string fullFilename = Path.Combine(path, filename);
            return fullFilename;
        }

        public string WriteFile(byte[] dataArray, string filename)
        {
            System.Diagnostics.Debug.WriteLine("Trying to write file");
            string fullFilename = GetFullFilePath(filename);
            System.Diagnostics.Debug.WriteLine(fullFilename);

            File.WriteAllBytes(fullFilename, dataArray);
            return fullFilename;
        }

        public void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        public void ClearFolder(string folder)
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
    }
}