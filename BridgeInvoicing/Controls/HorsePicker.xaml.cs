using BridgeInvoicing.Domain;
using BridgeInvoicing.ViewModels;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace BridgeInvoicing.Controls
{
    public partial class HorsePicker : ContentView
    {
        private bool itemSelected;
        private ObservableCollection<Horse> horseCollection;
        public HorsePicker()
        {
            InitializeComponent();
            horseCollection = new ObservableCollection<Horse>();
            Options.ItemsSource = horseCollection;
            Options.IsVisible = false;
            VerticalOptions = LayoutOptions.FillAndExpand;
            Options.ItemSelected += OnItemSelected;
            ClearName.ClearAction += ClearName_ClearAction;
            var currentMargin = Options.Margin;
            currentMargin.Top = -15;
            Options.Margin = currentMargin;
        }

        public bool ShowLabel
        {
            get { return NameLabel.IsVisible; }
            set { NameLabel.IsVisible = value; }
        }

        private void ClearName_ClearAction(object sender, EventArgs e)
        {
            ClearInput();
        }

        public Horse SelectedItem
        {
            get { return Options.SelectedItem == null ? null : (Horse)Options.SelectedItem; }
            set { Options.SelectedItem = value; }
        }

        public event EventHandler<SelectedItemChangedEventArgs> OnSelected;
        async void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (itemSelected)
            {
                itemSelected = false;
                return;
            }
            if (Name.Text.Length == 0)
            {
                ClearStudentCollection();
            }
            else if (Name.Text.Length >= 3)
            {
                var options = await App.Database.SearchHorseName(Name.Text);
                var optionsAvailable = options.Count > 0;
                if (optionsAvailable)
                {
                    horseCollection.Clear();

                    foreach (var item in options)
                    {
                        horseCollection.Add(item);
                    }
                    //Options.HeightRequest = 20 * options.Count;


                }
                Options.IsVisible = optionsAvailable;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            itemSelected = true;
            Options.IsVisible = false;
            Name.Text = ((Horse)args.SelectedItem).Name;
            if (OnSelected != null)
                OnSelected(this, args);
        }

        internal void ClearInput()
        {
            Name.TextChanged -= OnTextChanged;
            Name.Text = string.Empty;
            Name.TextChanged += OnTextChanged;
            ClearStudentCollection();
        }

        private void ClearStudentCollection()
        {
            Options.ItemSelected -= OnItemSelected;
            horseCollection.Clear();
            itemSelected = false;
            Options.SelectedItem = null;
            Options.IsVisible = false;
            Options.ItemSelected += OnItemSelected;
        }
    }
}
