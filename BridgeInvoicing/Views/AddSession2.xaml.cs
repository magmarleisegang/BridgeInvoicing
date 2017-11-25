using BridgeInvoicing.Domain;
using BridgeInvoicing.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;

namespace BridgeInvoicing.Views
{
    public partial class AddSession2 : ContentPage
    {
        public AddSession2()
        {
            Title = "Add";
            InitializeComponent();
            BindingContext = new AddSessionModel();
        }

        void OnStudentSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((AddSessionModel)BindingContext).Student = StudentOptions.SelectedItem;
        }

        void OnHorseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((AddSessionModel)BindingContext).Session.Horse = HorseOptions.SelectedItem.Name;
        }

        async void OnAddSessionClicked(object sender, EventArgs args)
        {
            Student student = GetStudentInput();
            Session newSession = GetSessionInput();
            newSession.SetStudent(student);

            var resultCode = await App.Database.AddSession(newSession);
            if (resultCode == 1)
            {
                await DisplayAlert("Done!", "Session saved successfully", "Ok, thanks");
                ClearInput();
            }
            else
            {
                await DisplayAlert("Failed", "Failed to save session", "Argh :(");
            }
        }

        private Session GetSessionInput()
        {
            Session newSession = ((AddSessionModel)BindingContext).Session;
            return newSession;
        }

        private Student GetStudentInput()
        {
            Student student = ((AddSessionModel)BindingContext).Student;
            if (StudentOptions.SelectedItem == null)
            {
                student.Name = StudentOptions.Text;
            }
            else
            {
                student.Id = StudentOptions.SelectedItem.Id;
                student.Name = StudentOptions.SelectedItem.Name;
            }
            return student;
        }

        public void ClearInput()
        {
            //ClearStudentInput();
            //ClearSessionInput();
            StudentOptions.ClearInput();
            HorseOptions.ClearInput();
            BindingContext = new AddSessionModel();
            

        }

        private void ClearSessionInput()
        {
            BindingContext = new AddSessionModel();
            //HorseName.TextChanged -= OnHorseTextChanged;
            //HorseName.Text = string.Empty;
            //HorseName.TextChanged += OnHorseTextChanged;
            //HorseOptions.IsVisible = false;
            //Date.Date = System.DateTime.Now;
            //Time.Time = System.DateTime.Now.TimeOfDay;
            //Comment.Text = string.Empty;
            //Charge.Text = string.Empty;
        }

        private void ClearStudentInput()
        {
            //StudentOptions.ClearInput();
            //StudentEmail.Text = string.Empty;
            //StudentPhone.Text = string.Empty;
        }
    }
}
