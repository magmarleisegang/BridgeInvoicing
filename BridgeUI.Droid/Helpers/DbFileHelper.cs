using BridgeInvoicing.Helpers;
using BridgeUI.Droid.Helpers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbFileHelper))]
namespace BridgeUI.Droid.Helpers
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