using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;
using OctoPrintCore;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using System.Threading.Tasks;
using MvvmCross;
using OctoPrintCore.Services;
using Octoprint.Services;
using Firebase;

namespace Octoprint
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication()
        {
        }

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);
            ZXing.Mobile.MobileBarcodeScanner.Initialize(this);

            FirebaseApp.InitializeApp(Application.Context);
        }
    }
}