namespace BridgeInvoicing.Emails
{
    public interface IEmailSender
    {
        void SendEmail(string toAddress, string subject, string body, string attachmentFileName);
    }
}
