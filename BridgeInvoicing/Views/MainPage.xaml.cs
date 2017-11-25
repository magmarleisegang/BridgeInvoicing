using System;
using Xamarin.Forms;
using appSettings = BridgeInvoicing.Helpers.Settings;

namespace BridgeInvoicing.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            Title = "HelloS";
            bool user2 = true;
            if (user2)
            {
                Children.Add(new AddSession2());
                Children[0].Appearing += MainPage_Appearing;
            }
            else
            {
                Children.Add(new AddSession());
            }

            Children.Add(new ListSessions());
            Children.Add(new Settings());
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
