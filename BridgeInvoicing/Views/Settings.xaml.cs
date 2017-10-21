using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridgeInvoicing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        async void OnUpload(object sender, EventArgs e)
        {
            //try
            //{
            System.Diagnostics.Debug.WriteLine("file picked");
            FileData filedata = await CrossFilePicker.Current.PickFile();
            // the dataarray of the file will be found in filedata.DataArray 
            // file name will be found in filedata.FileName;
            //etc etc.
            var fileWriter = DependencyService.Get<IFileHelper>();
            fileWriter.WriteFile(filedata.DataArray, BridgeInvoicing.Helpers.Settings.InvoiceTemplateFile);

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandler.ShowException(ex.Message);
            //}
        }
    }
}