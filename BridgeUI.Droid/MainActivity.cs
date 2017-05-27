using Android.App;
using Android.Widget;
using Android.OS;
using BridgeInvoicing.Data;
using BridgeInvoicing.Domain;
using System;
using BridgeInvoicing;

namespace BridgeUI.Droid
{
    [Activity(Label = "BridgeUI.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        
    }
}

