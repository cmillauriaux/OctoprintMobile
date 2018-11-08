using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using OctoPrintCore.ViewModels;

namespace Octoprint
{
    public class PrintFragment : MvxFragment<PrintViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            this.EnsureBindingContextIsSet(inflater);
            return this.BindingInflate(Resource.Layout.fragment_print, container, false);
        }
    }
}