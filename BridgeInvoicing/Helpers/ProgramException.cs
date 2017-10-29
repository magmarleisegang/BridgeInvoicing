using Xamarin.Forms;

namespace BridgeInvoicing.Helpers
{
    public static class LogicErrorHandler
    {
        internal static void LogicErrorAlert(this ContentPage listSessions, string mesage)
        {
            listSessions.DisplayAlert("Can't do that...", mesage, "Damn, ok");
        }
    }
}
