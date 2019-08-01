namespace BridgeInvoicing.Helpers
{
    public interface IInvoiceFileHelper
    {
        string CreateTempInvoiceAttachment(string filename, string content);
        string GetTemplateFile(string filename);
        string WriteTemplateFile(byte[] dataArray, string filename);
        void ClearInvoiceAttachmentFolder(string tempInvoiceFolder);
        bool InvoiceTemplateFileExists(string filename);
    }
}
