using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using OctoPrintCore.ViewModels;

namespace Octoprint.Views
{
    [Activity(Label = "Connection", MainLauncher = true)]
    public class ConnectionView : MvxActivity<ConnectionViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}