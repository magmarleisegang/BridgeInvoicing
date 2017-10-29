using Android.App;
using Android.Content;
using BridgeInvoicing;
using BridgeUI.Driod;
using BridgeUI.Driod.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmailSender))]
namespace BridgeUI.Driod
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toAddress, string subject, string body, string attachmentFileName)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Intent.ExtraEmail, new string[] { toAddress });
            email.PutExtra(Intent.ExtraSubject, subject);
            email.PutExtra(Intent.ExtraText, body);
            if (!string.IsNullOrEmpty(attachmentFileName))
            {
                email.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("content://" + CachedFileProvider.AUTHORITY + "/" + attachmentFileName));

                //File fileIn = new File(attachmentFileName);
                //fileIn.SetReadable(true, false);
                //email.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(fileIn));
                //fileIn.DeleteOnExit();
            }
            email.SetType("text/html");
            email.SetFlags(ActivityFlags.NewTask);
            ((Activity)Forms.Context).StartActivityForResult(email, 0);
            //Android.App.Application.Context.StartActivityWithResult(email);           
        }
    }
}