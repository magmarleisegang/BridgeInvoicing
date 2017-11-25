
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridgeInvoicing.Helpers
{
    public class DecimalConverter : IMarkupExtension, IValueConverter
    {
        //Convert value from VM type to View type i.e. string for Entry
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        //Convert value from View type to VM type i.e. from string for Entry
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString();
            if (string.IsNullOrEmpty(stringValue))
                return 0;
            return decimal.Parse(stringValue);
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
