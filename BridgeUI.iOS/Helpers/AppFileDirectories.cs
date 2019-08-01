using System;
using System.Collections.Generic;
using System.Text;

namespace BridgeUI.iOS.Helpers
{
    public static class AppFileDirectories
    {
        public static string Library = "Library";
        public static string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string Temp = "tmp";
        public static string Invoices = "Invoices";
        public static string InvoiceTemplate = "InvoiceTemplate";

        public static string Database = "Database";
    }
}
