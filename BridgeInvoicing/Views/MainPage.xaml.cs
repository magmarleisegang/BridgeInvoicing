using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appSettings = BridgeInvoicing.Helpers.Settings;
using Xamarin.Forms;

namespace BridgeInvoicing.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            Title = "HelloS";
            Children.Add(new AddSession2());
            Children.Add(new ListSessions());
            Children.Add(new Settings());
            Children[0].Appearing += MainPage_Appearing; ;
            //ClearTempInvoiceFolder();
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            ((AddSession2)sender).ClearInput();
        }

        private void ClearTempInvoiceFolder()
        {
                var fileWriter = DependencyService.Get<IFileHelper>();
            fileWriter.ClearFolder(appSettings.TempInvoiceFolder);
        }
    }
}
