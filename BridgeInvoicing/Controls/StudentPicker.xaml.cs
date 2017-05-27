using BridgeInvoicing.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BridgeInvoicing.Controls
{
    public partial class StudentPicker : ContentView
    {
        private bool studentSelected;
        private ObservableCollection<Student> studentCollection;
        public StudentPicker()
        {
            InitializeComponent();
            studentCollection = new ObservableCollection<Student>();
            StudentOptions.ItemsSource = studentCollection;
            StudentOptions.IsVisible = false;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }

        public Student SelectedItem
        {
            get { return StudentOptions.SelectedItem == null ? null : (Student)StudentOptions.SelectedItem; }
            set { StudentOptions.SelectedItem = value; }
        }
        public string Text
        {
            get { return StudentName.Text; }
            set { StudentName.Text = value; }
        }

        public int? SelectedStudentId
        {
            get
            {
                return SelectedItem == null ? (int?)null : SelectedItem.Id;
            }
        }

        public event EventHandler<SelectedItemChangedEventArgs> OnSelected;
        async void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (studentSelected)
            {
                studentSelected = false;
                return;
            }
            if (StudentName.Text.Length == 0)
            {
                ClearStudentCollection();
            }
            else if (StudentName.Text.Length >= 3)
            {
                var options = await App.Database.SearchStudent(StudentName.Text);
                var optionsAvailable = options.Count > 0;
                if (optionsAvailable)
                {
                    studentCollection.Clear();
                    foreach (var item in options)
                    {
                        studentCollection.Add(item);
                    }
                }
                StudentOptions.IsVisible = optionsAvailable;
            }            
        }


        async void OnStudentSelected(object sender, SelectedItemChangedEventArgs args)
        {
            studentSelected = true;
            StudentOptions.IsVisible = false;
            StudentName.Text = ((Student)args.SelectedItem).Name;
            if (OnSelected != null)
                OnSelected(this, args);
        }

        internal void ClearInput()
        {
            StudentName.TextChanged -= OnTextChanged;
            StudentName.Text = string.Empty;
            StudentName.TextChanged += OnTextChanged;
            ClearStudentCollection();
        }

        private void ClearStudentCollection()
        {
            studentCollection.Clear();
            studentSelected = false;
            StudentOptions.SelectedItem = null;
            StudentOptions.IsVisible = false;
        }
    }
}
