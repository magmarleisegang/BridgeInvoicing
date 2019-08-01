using Android.App;
using Android.Content;
using BridgeInvoicing.Emails;
using BridgeUI.Driod.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmailSender))]
namespace BridgeUI.Driod.Helpers
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toAddress, string subject, string body, string attachmentFileName)
        {
            var emailIntent = new Intent(Android.Content.Intent.ActionSend);
            emailIntent.SetFlags(ActivityFlags.NewTask);
            emailIntent.AddFlags(ActivityFlags.GrantReadUriPermission);
            emailIntent.PutExtra(Intent.ExtraEmail, new string[] { toAddress });
            emailIntent.PutExtra(Intent.ExtraSubject, subject);
            emailIntent.PutExtra(Intent.ExtraText, body);
            if (!string.IsNullOrEmpty(attachmentFileName))
            {               
                var attachementUri = Android.Net.Uri.Parse(CachedFileProvider.CONTENT_URI + "/" + attachmentFileName);
                
                emailIntent.PutExtra(Intent.ExtraStream, attachementUri);
                emailIntent.SetType("text/html");
            }

            ((Activity)Forms.Context).StartActivityForResult(emailIntent, 0);
        }
    }
}