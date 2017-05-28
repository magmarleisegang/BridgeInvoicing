using BridgeInvoicing.Domain;
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

        async Task LoadList()
        {
            sessionListCollection.Clear();
            var student = StudentFilter.SelectedStudentId;
            var list = await App.Database.GetAllSessions(new DateTime(2017, 01, 01), DateTime.Now, student);
            if (list != null)
            {
                var grouped = list.GroupBy<Session, int>(x =>  x.StudentId);
                foreach (var studentList in grouped)
                {
                    sessionListCollection.Add(new SessionListGroup(studentList));
                }               
            }
            return;
        }
    }
}
