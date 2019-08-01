using BridgeInvoicing.Helpers;
using BridgeUI.iOS.Helpers;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbFileHelper))]
namespace BridgeUI.iOS.Helpers
{
    public class DbFileHelper : IDbFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var documents = AppFileDirectories.MyDocuments;
            var libraryDatabase = Path.Combine(documents, "..", AppFileDirectories.Library, AppFileDirectories.Database);
            return Path.Combine(libraryDatabase, filename);
        }
    }
}
