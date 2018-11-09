using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using OctoPrintCore.ViewModels;
using UIKit;

namespace OctoPrintiOS.Views
{
    public partial class ConnectionView : MvxViewController<ConnectionViewModel>
    {
        public ConnectionView() : base(nameof(ConnectionView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<ConnectionView, ConnectionViewModel>();
            set.Apply();
        }
    }
}