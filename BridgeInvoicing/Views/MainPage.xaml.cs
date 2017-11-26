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

            Children.Add(new AddSession2());
            Children[0].Appearing += MainPage_Appearing;

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
