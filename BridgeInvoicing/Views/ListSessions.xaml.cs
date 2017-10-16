﻿using BridgeInvoicing.Domain;
using BridgeInvoicing.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

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
            //ListAll();
        }

        async void OnLoadClicked(object sender, EventArgs args)
        {
            await LoadList();
        }

        void OnEmailClicked(object sender, EventArgs args)
        {
            var studentId = StudentFilter.SelectedStudentId;
            if (studentId.HasValue)
            {
                var list = App.Database.GetAllSessions(new DateTime(2017, 01, 01), DateTime.Now, studentId).Result;
                var student = App.Database.GetStudentById(studentId.Value).Result;
                var invoiceEmail = new Emails.Invoice();
                    var fileWriter = DependencyService.Get<IFileHelper>();
               
                invoiceEmail
                        .To(student)
                        .Sessions(list)
                        .LoadTemplate(fileWriter.GetFile(INVOICE_TEMPLATE_FILE));

                var emailSender = DependencyService.Get<IEmailSender>();

                var attachment = fileWriter.WriteFile(invoiceEmail.GetHtmlEmail(), "invoice.html");
                emailSender.SendEmail(student.Email, "test", "test body", attachment);
            }
        }

        async Task LoadList()
        {
            sessionListCollection.Clear();
            var studentId = StudentFilter.SelectedStudentId;
            var list = await App.Database.GetAllSessions(new DateTime(2017, 01, 01), DateTime.Now, studentId);
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
