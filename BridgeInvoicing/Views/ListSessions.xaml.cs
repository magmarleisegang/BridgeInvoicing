using BridgeInvoicing.Domain;
using BridgeInvoicing.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using appSettings = BridgeInvoicing.Helpers.Settings;
using BridgeInvoicing.Helpers;

namespace BridgeInvoicing.Views
{
    public partial class ListSessions : ContentPage
    {
        ObservableCollection<SessionListGroup> sessionListCollection;
        private const string INVOICE_TEMPLATE_FILE = "invoice.html";

        public ListSessions()
        {
            Title = "View All";

            InitializeComponent();
            sessionListCollection = new ObservableCollection<SessionListGroup>();
            SessionsList.ItemsSource = sessionListCollection;
            FromDate.DateSelected += FromDate_DateSelected;
        }

        private void FromDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            ToDate.MinimumDate = FromDate.Date;
        }

        async void OnLoadClicked(object sender, EventArgs args)
        {
            await LoadList();
        }

        void OnEmailClicked(object sender, EventArgs args)
        {
            var studentId = StudentFilter.SelectedStudentId;
            if (!studentId.HasValue)
            {
                this.LogicErrorAlert("Please search & select a student");
                return;
            }

            var fileWriter = DependencyService.Get<IFileHelper>();
            if (!fileWriter.FileExists(appSettings.InvoiceTemplateFile))
            {
                this.LogicErrorAlert("No invoice template available");
                return;
            }

            try
            {
                var list = App.Database.GetAllSessions(new DateTime(2017, 01, 01), DateTime.Now, studentId).Result;
                var student = App.Database.GetStudentById(studentId.Value).Result;
                var invoiceEmail = new Emails.Invoice(string.Format("{0}-{1:MMM-yyyy}", student.Name, DateTime.Now));

                invoiceEmail
                        .To(student)
                        .Sessions(list)
                        .LoadTemplate(fileWriter.GetFile(appSettings.InvoiceTemplateFile));

                var emailSender = DependencyService.Get<IEmailSender>();
                var filename = invoiceEmail.InvoiceNumber + ".html";
                var attachmentFileName = fileWriter.CreateTempFile(invoiceEmail.InvoiceNumber + ".html", invoiceEmail.BuildHtml());
                emailSender.SendEmail(student.Email, invoiceEmail.InvoiceNumber, appSettings.DefaultInvoiceMessage, filename);
            }
            catch (Exception ex)
            {
                this.LogicErrorAlert(ex.Message);
            }
        }

        async Task LoadList()
        {
            sessionListCollection.Clear();
            var studentId = StudentFilter.SelectedStudentId;
            var fromDate = FromDate.Date.Date;
            var toDate = ToDate.Date.Date.AddDays(1);
            var list = await App.Database.GetAllSessions(fromDate, toDate, studentId);
            if (list != null)
            {
                var grouped = list.GroupBy<Session, int>(x => x.StudentId);
                foreach (var studentList in grouped)
                {
                    var student = await App.Database.GetStudentById(studentList.Key);
                    sessionListCollection.Add(new SessionListGroup(student, studentList.ToList()));
                }
            }
            return;
        }
    }
}
