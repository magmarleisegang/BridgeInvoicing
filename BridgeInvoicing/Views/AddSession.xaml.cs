using BridgeInvoicing.Domain;
using System;
using System.Linq;

using Xamarin.Forms;

namespace BridgeInvoicing.Views
{
    public partial class AddSession : ContentPage
    {
        public AddSession()
        {
            Title = "Add";
            InitializeComponent();
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

        async void OnAddSessionClicked(object sender, EventArgs args)
        {
            Student student = GetStudentInput();
            Session newSession = GetSessionInput();
            if (student.IsValid() && newSession.IsValid())
            {
                if (student.IsNew() || student.IsChanged())
                {
                   await App.Database.SaveStudent(student);
                }
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
            else
            {
                await DisplayAlert("Failed", "Failed to save session. Some inputs are invalid.", "Ok, I'll fix it.");
            }

        }

        private Session GetSessionInput()
        {
            Session newSession = new Session();
            newSession.Horse = HorseOptions.SelectedItem.Name;
            newSession.Date = Date.Date.Add(Time.Time);

            if (!string.IsNullOrEmpty(Charge.Text))
            { newSession.Price = decimal.Parse(Charge.Text); }

            if (!string.IsNullOrEmpty(Comment.Text))
            { newSession.Comment = Comment.Text; }

            return newSession;
        }

        private Student GetStudentInput()
        {
            Student student;
            if (StudentOptions.SelectedItem == null)
            {
                student = new Student();
                student.Name = StudentOptions.Text;
                student.SetEmail(StudentEmail.Text);
                student.SetPhone(StudentPhone.Text);
            }
            else
            {
                student = (Student)StudentOptions.SelectedItem;
                if (!student.Email.Equals(StudentEmail.Text, StringComparison.CurrentCultureIgnoreCase))
                    student.SetEmail(StudentEmail.Text);

                if (!student.Phone.Equals(StudentPhone.Text, StringComparison.CurrentCultureIgnoreCase))
                    student.SetPhone(StudentPhone.Text);
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
            HorseOptions.ClearInput();
            Date.Date = System.DateTime.Now;
            Time.Time = System.DateTime.Now.TimeOfDay;
            Comment.Text = string.Empty;
            Charge.Text = Helpers.Settings.DefaultRate.ToString();
        }

        private void ClearStudentInput()
        {
            StudentOptions.ClearInput();
            StudentEmail.Text = string.Empty;
            StudentPhone.Text = string.Empty;
        }
    }
}
