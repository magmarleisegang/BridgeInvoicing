using System;
//using System.IO;
using BridgeUI.Droid;
using BridgeInvoicing;
using Xamarin.Forms;
using BridgeUI.Driod;
using Android.Content;
using Android.Text;
using Java.IO;

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
                File fileIn = new File(attachmentFileName);
                fileIn.SetReadable(true, false);
                email.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(fileIn));
            }
            email.SetType("text/html");
            email.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(email);
        }
    }
}