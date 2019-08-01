using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Helpers
{
    internal static class Extensions
    {
        public static bool CheckNullString(this string valueToCheck)
        {
            return !string.IsNullOrEmpty(valueToCheck);
        }

        public static bool CheckDecimalString(this string valueToCheck)
        {
            if (string.IsNullOrEmpty(valueToCheck))
                return false;
            decimal result;
            return (decimal.TryParse(valueToCheck, out result));
        }
    }
}
