using BridgeInvoicing.Domain;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BridgeInvoicing.ViewModels
{
    public class AddSessionModel : BindableObject
    {
        public AddSessionModel()
        {
            session = new Session();
            session.Date = DateTime.Now;
            session.Price = Helpers.Settings.DefaultRate;
            student = new Student();
        }

        Session session;
        public Session Session
        {
            get { return session; }
            set
            {
                session = value;
                this.OnPropertyChanged();
            }
        }

        Student student;

        public Student Student
        {
            get { return student; }
            set
            {
                student = value;
                this.OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return student.Email; }
            set { student.Email = value; }
        }

        public string Phone
        {
            get { return student.Phone; }
            set { student.Phone = value; }
        }

        public string Horse
        {
            get { return Session.Horse; }
            set { Session.Horse = value; }
        }

        public DateTime Date
        {
            get { return Session.Date; }
            set { Session.Date = value; }
        }

        public TimeSpan Time
        {
            get { return Session.Date.TimeOfDay; }
            set { Session.Date = Session.Date.Date.Add(value); }
        }

        public decimal Charge
        {
            get { return Session.Price ?? 0; }
            set { Session.Price = value; }
        }

        public string Comment
        {
            get { return Session.Comment; }
            set { Session.Comment = value; }
        }
    }
}