using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BridgeInvoicing.Controls;
using Xamarin.Forms;
using BridgeUI.Driod.Renderes;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NestedListView), typeof(NestedListViewRenderer))]
namespace BridgeUI.Driod.Renderes
{
    public class NestedListViewRenderer : ListViewRenderer
    {
        public NestedListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var listView = this.Control as Android.Widget.ListView;
                //listView.NestedScrollingEnabled = true;
            }
        }
    }
}