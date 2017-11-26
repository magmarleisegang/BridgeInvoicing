using Xamarin.Forms;
using AppSettings = BridgeInvoicing.Helpers.Settings;

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
    }
}
