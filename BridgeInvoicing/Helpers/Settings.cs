// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BridgeInvoicing.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string invoiceTemplateKey = "settings_invoiceTemplate";
        private static readonly string invoiceTemplate = "invoice.html";

        private const string tempInvoiceFolderKey = "settings_tempInvoiceFolder";
        private static readonly string tempInvoiceFolder = "invoices";

        private const string defaultRateKey = "settings_defautrate";

        private const string defaultInvoiceMessageKey = "settings_defaultInvoiceMessage";
        #endregion


        public static string InvoiceTemplateFile
        {
            get
            {
                return AppSettings.GetValueOrDefault(invoiceTemplateKey, invoiceTemplate);
            }
            set
            {
                AppSettings.AddOrUpdateValue(invoiceTemplateKey, value);
            }
        }
        public static string TempInvoiceFolder
        {
            get
            {
                return AppSettings.GetValueOrDefault(tempInvoiceFolderKey, tempInvoiceFolder);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tempInvoiceFolderKey, value);
            }
        }

        public static decimal? DefaultRate
        {
            get
            {
                var defaultRateSetting = AppSettings.GetValueOrDefault(defaultRateKey, "0.00");
                decimal temp;
                if (decimal.TryParse(defaultRateSetting, out temp))
                    return temp;
                return null;
            }
            set
            {
                AppSettings.AddOrUpdateValue(defaultRateKey, value.ToString());
            }
        }

        public static string DefaultInvoiceMessage
        {
            get
            {
                return AppSettings.GetValueOrDefault(defaultInvoiceMessageKey, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(defaultInvoiceMessageKey, value);
            }
        }
    }
}