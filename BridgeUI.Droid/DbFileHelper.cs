using System;
using System.IO;
using BridgeUI.Droid;
using BridgeInvoicing;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbFileHelper))]
namespace BridgeUI.Droid
{
    public class DbFileHelper : IDbFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}