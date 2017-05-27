using BridgeInvoicing.Domain;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BridgeInvoicing.Views
{
    public partial class ListSessions : ContentPage
    {
        ObservableCollection<Session> sessionListCollection;
        public ListSessions()
        {
            Title = "View All";

            InitializeComponent();
            sessionListCollection = new ObservableCollection<Session>();
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
                list.Sort(new SessionDateComparer());
                foreach (var item in list)
                {
                    sessionListCollection.Add(item);
                }
            }
            return;
        }
    }
}
