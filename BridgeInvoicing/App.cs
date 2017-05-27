using BridgeInvoicing.Data;
using BridgeInvoicing.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BridgeInvoicing
{
    public class App : Application
    {
        static Database database;

        public App()
        {
            ////var np = new NavigationPage(new MainPage());
            //MainPage = np;
            MainPage = new MainPage();
        }


        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(DependencyService.Get<IDbFileHelper>().GetLocalFilePath("BridgeInvSqLite.db3"));
                }
                return database;
            }
        }
    }
}
