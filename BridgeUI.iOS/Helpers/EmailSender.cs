using BridgeInvoicing.Emails;
using BridgeUI.iOS.Helpers;
using MessageUI;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmailSender))]
namespace BridgeUI.iOS.Helpers
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toAddress, string subject, string body, string attachmentFileName)
        {
            if (MFMailComposeViewController.CanSendMail)
            {
                var mailController = new MFMailComposeViewController();
                // do mail operations here

                mailController.SetToRecipients(new string[] { toAddress });
                mailController.SetSubject(subject);
                mailController.SetMessageBody(body, false);
                //Handle the Finished event.
                mailController.Finished += (object s, MFComposeResultEventArgs args) =>
                {
                    Console.WriteLine(args.Result.ToString());
                    args.Controller.DismissViewController(true, null);
                };
                //Present the MFMailComposeViewController.
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true, null);
            }
            else

            {
                new UIAlertView("Mail not supported", "Can't send mail from this device", null, "OK");
            }
        }
    }
}
