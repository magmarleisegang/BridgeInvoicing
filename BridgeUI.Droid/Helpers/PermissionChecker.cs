using Android.App;
using Android.Support.V4.App;
using BridgeInvoicing.Helpers;
using BridgeUI.Driod.Helpers;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(PermissionChecker))]
namespace BridgeUI.Driod.Helpers
{
    public class PermissionChecker : IPermissionChecker
    {
        public bool FileAccessPermission()
        {
            var mContext = Forms.Context;
            var readPermission = Android.Manifest.Permission.ReadExternalStorage;
            if (ActivityCompat.CheckSelfPermission((Activity)mContext, readPermission) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity)mContext, new String[] { readPermission }, 10);
            }
            return true;
        }

    }
}