using BridgeInvoicing.Domain;
using System;
using System.Linq;

using Xamarin.Forms;

namespace BridgeInvoicing.Views
{
    public partial class AddSession : ContentPage
    {
        private bool horseSelected;
        public AddSession()
        {
            Title = "Add";
            InitializeComponent();
            HorseOptions.IsVisible = false;
            Date.Date = System.DateTime.Now;
            Time.Time = System.DateTime.Now.TimeOfDay;
        }

        void OnStudentSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var student = StudentOptions.SelectedItem;
            SetStudentInput(student);
        }

        private void SetStudentInput(Student student)
        {
            StudentEmail.Text = student.Email;
            StudentPhone.Text = student.Phone;
        }

        async void OnHorseTextChanged(object sender, TextChangedEventArgs args)
        {
            if (horseSelected)
            {
                horseSelected = false;
                return;
            }
            Entry horseName = (Entry)sender;
            if (horseName.Text.Length > 3)
            {
                var options = await App.Database.SearchHorseName(horseName.Text);
                var optionsAvailable = options.Count > 0;
                if (optionsAvailable)
                    HorseOptions.ItemsSource = options.Select(x => x.Name);
                HorseOptions.IsVisible = optionsAvailable;
            }
        }

        void OnHorseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            horseSelected = true;
            HorseName.Text = HorseOptions.SelectedItem.ToString();
            HorseOptions.IsVisible = false;
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
            Session newSession = new Session();
            newSession.Horse = HorseName.Text;
            if (!string.IsNullOrEmpty(Charge.Text))
                newSession.Price = decimal.Parse(Charge.Text);
            newSession.Date = Date.Date.Add(Time.Time);
            return newSession;
        }

        private Student GetStudentInput()
        {
            Student student;
            if (StudentOptions.SelectedItem == null)
            {
                student = new Student();
                student.Name = StudentOptions.Text;
                student.Email = StudentEmail.Text;
                student.Phone = StudentPhone.Text;
            }
            else
            {
                student = (Student)StudentOptions.SelectedItem;
                if (!student.Email.Equals(StudentEmail.Text, StringComparison.CurrentCultureIgnoreCase))
                    student.Email = StudentEmail.Text;
                if (!student.Phone.Equals(StudentPhone.Text, StringComparison.CurrentCultureIgnoreCase))
                    student.Phone = StudentPhone.Text;
            }
            return student;
        }

        private void ClearInput()
        {
            ClearStudentInput();
            ClearSessionInput();
        }

        private void ClearSessionInput()
        {
            HorseName.TextChanged -= OnHorseTextChanged;
            HorseName.Text = string.Empty;
            HorseName.TextChanged += OnHorseTextChanged;
            HorseOptions.IsVisible = false;
            Date.Date = System.DateTime.Now;
            Time.Time = System.DateTime.Now.TimeOfDay;
        }

        private void ClearStudentInput()
        {
            StudentOptions.ClearInput();
            StudentEmail.Text = string.Empty;
            StudentPhone.Text = string.Empty;
        }
    }
}
