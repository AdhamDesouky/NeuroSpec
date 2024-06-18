using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System;

namespace NeuroSpecCompanion
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private const int RequestCallPermissionCode = 101;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Your initialization code
        }

        private void RequestCallPermission()
        {
            if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.CallPhone) == Permission.Granted)
            {
                // Permission already granted, proceed with the operation
                MakePhoneCall();
            }
            else
            {
                // Permission is not granted, request it
                ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.CallPhone }, RequestCallPermissionCode);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestCallPermissionCode)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    // Permission granted, proceed with the operation
                    MakePhoneCall();
                }
                else
                {
                    // Permission denied, show a message or handle accordingly
                    Toast.MakeText(this, "Permission denied to make a phone call", ToastLength.Short).Show();
                }
            }
        }

        private void MakePhoneCall()
        {
            try
            {
                Intent callIntent = new Intent(Intent.ActionCall);
                callIntent.SetData(Android.Net.Uri.Parse("tel:01115008374")); // Replace with the actual number
                StartActivity(callIntent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Short).Show();
            }
        }
    }
}
