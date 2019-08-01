using Xamarin.Forms;
using AppSettings = BridgeInvoicing.Helpers.Settings;
using System.IO;

namespace BridgeInvoicing.ViewModels
{
    public class SettingsViewModel : BindableObject
    {
        public SettingsViewModel()
        { }

        public decimal DefaultRate
        {
            get
            {
                return AppSettings.DefaultRate ?? 0;
            }
            set
            {
                if (AppSettings.DefaultRate != value)
                {
                    AppSettings.DefaultRate = value;
                }
            }
        }

        public string DefaultInvoiceMessage
        {
            get
            {
                return AppSettings.DefaultInvoiceMessage;
            }
            set
            {
                if (AppSettings.DefaultInvoiceMessage != value)
                {
                    AppSettings.DefaultInvoiceMessage = value;
                }
            }
        }
        public bool InvoiceTemplateExists
        {
            get; set;
        }

        public string InvoiceTemplateExistsDisplay {
            get { return InvoiceTemplateExists ? "Yes" : "No"; }
        }

        public string InvoiceTemplateFileName
        {
            get { return AppSettings.InvoiceTemplateFile; }
        }
    }
}
