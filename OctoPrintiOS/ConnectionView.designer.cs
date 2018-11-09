// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace OctoPrintiOS
{
    [Register ("ConnectionView")]
    partial class ConnectionView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView LogoImageView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LogoImageView != null) {
                LogoImageView.Dispose ();
                LogoImageView = null;
            }
        }
    }
}