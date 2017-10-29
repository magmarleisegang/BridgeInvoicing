using BridgeInvoicing.Helpers;
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

            System.Diagnostics.Debug.WriteLine("file picked");
            FileData filedata = await CrossFilePicker.Current.PickFile();
            // the dataarray of the file will be found in filedata.DataArray 
            // file name will be found in filedata.FileName;
            //etc etc.
            if (filedata != null)
            {
                var fileWriter = DependencyService.Get<IFileHelper>();
                fileWriter.WriteFile(filedata.DataArray, BridgeInvoicing.Helpers.Settings.InvoiceTemplateFile);
                if (!fileWriter.FileExists(BridgeInvoicing.Helpers.Settings.InvoiceTemplateFile))
                {
                    this.LogicErrorAlert("Failed to save invoice template");
                }
            }
        }
    }
}