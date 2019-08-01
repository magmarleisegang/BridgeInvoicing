using BridgeInvoicing.Helpers;
using BridgeInvoicing.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppSettings = BridgeInvoicing.Helpers.Settings;

namespace BridgeInvoicing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            var settings = new SettingsViewModel();
            var fileWriter = DependencyService.Get<IInvoiceFileHelper>();
            settings.InvoiceTemplateExists = fileWriter.InvoiceTemplateFileExists(settings.InvoiceTemplateFileName);
            BindingContext = settings;
        }

        async void OnUpload(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("file picked");
            var permissions = DependencyService.Get<IPermissionChecker>();
            permissions.FileAccessPermission();
            FileData filedata = await CrossFilePicker.Current.PickFile();
            // the dataarray of the file will be found in filedata.DataArray 
            // file name will be found in filedata.FileName;
            //etc etc.
            if (filedata != null)
            {
                var fileWriter = DependencyService.Get<IInvoiceFileHelper>();
                if (filedata.DataArray.Length < 1)
                {
                    this.LogicErrorAlert("Could not access template file");
                    return;
                }
                fileWriter.WriteTemplateFile(filedata.DataArray, AppSettings.InvoiceTemplateFile);
                if (!fileWriter.InvoiceTemplateFileExists(AppSettings.InvoiceTemplateFile))
                {
                    this.LogicErrorAlert("Failed to save invoice template");
                }
                else
                {
                    ((SettingsViewModel)BindingContext).InvoiceTemplateExists = true;
                }
            }
            else
            {
                this.LogicErrorAlert("Could not access template file (Please check your permissions)");
            }
        }

        private void DefaultRate_Completed(object sender, EventArgs e)
        {

        }
    }
}