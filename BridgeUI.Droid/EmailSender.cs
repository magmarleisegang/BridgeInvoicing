using System;
using System.IO;
using BridgeUI.Droid;
using BridgeInvoicing;
using Xamarin.Forms;
using BridgeUI.Driod;
using Android.Content;
using Android.Text;

[assembly: Dependency(typeof(EmailSender))]
namespace BridgeUI.Driod
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toAddress, string subject, string body)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Intent.ExtraEmail, new string[] { toAddress });
            email.PutExtra(Intent.ExtraSubject, subject);
            email.PutExtra(Intent.ExtraText, Html.FromHtml(body));

            email.SetType("message/rfc822");
            email.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(email);
        }
    }
}