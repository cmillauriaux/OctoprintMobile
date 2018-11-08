using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using OctoPrintCore.ViewModels;

namespace Octoprint.Views
{
    [Activity(Label = "Connection")]
    public class MainView : MvxFragmentActivity<MainViewModel>, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_tab);

            // Make tab menu
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            // Put connection fragment
            PrinterConnectionFragment connection = new PrinterConnectionFragment() { ViewModel = Mvx.IoCConstruct<PrinterConnectionViewModel>() };
            var t = SupportFragmentManager.BeginTransaction();
            t.Add(Resource.Id.fragment_view, connection);
            t.Commit();

            ImageButton connectionIcon = FindViewById<ImageButton>(Resource.Id.connection_icon);
            connectionIcon.Click += ConnectionIcon_Click;
        }

        private void ConnectionIcon_Click(object sender, EventArgs e)
        {
            var t = SupportFragmentManager.BeginTransaction();
            PrinterConnectionFragment connection = new PrinterConnectionFragment() { ViewModel = Mvx.IoCConstruct<PrinterConnectionViewModel>() };
            t.Replace(Resource.Id.fragment_view, connection);
            t.Commit();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            var t = SupportFragmentManager.BeginTransaction();
            switch (item.ItemId)
            {
                case Resource.Id.navigation_print:
                    PrintFragment connection = new PrintFragment() { ViewModel = Mvx.IoCConstruct<PrintViewModel>() };
                    t.Replace(Resource.Id.fragment_view, connection);
                    t.Commit();
                    return true;
                case Resource.Id.navigation_control:
                    PrinterConnectionFragment navigation = new PrinterConnectionFragment() { ViewModel = Mvx.IoCConstruct<PrinterConnectionViewModel>() };
                    t.Replace(Resource.Id.fragment_view, navigation);
                    t.Commit();
                    return true;
                case Resource.Id.navigation_temperature:
                    PrinterConnectionFragment temperature = new PrinterConnectionFragment() { ViewModel = Mvx.IoCConstruct<PrinterConnectionViewModel>() };
                    t.Replace(Resource.Id.fragment_view, temperature);
                    t.Commit();
                    return true;
                case Resource.Id.navigation_terminal:
                    PrinterConnectionFragment terminal = new PrinterConnectionFragment() { ViewModel = Mvx.IoCConstruct<PrinterConnectionViewModel>() };
                    t.Replace(Resource.Id.fragment_view, terminal);
                    t.Commit();
                    return true;
                case Resource.Id.navigation_gcode:
                    AccountFragment gcode = new AccountFragment() { ViewModel = Mvx.IoCConstruct<AccountViewModel>() };
                    t.Replace(Resource.Id.fragment_view, gcode);
                    t.Commit();
                    return true;
            }
            return false;
        }
    }
}