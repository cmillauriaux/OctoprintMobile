using MvvmCross.Commands;
using MvvmCross.ViewModels;
using OctoPrintCore.Model;
using OctoPrintCore.Services;
using System;
using System.Threading.Tasks;

namespace OctoPrintCore.ViewModels
{
    public class OctoprintInstanceViewModel : MvxViewModel
    {
        public OctoprintInstanceViewModel()
        {
        }

        public override async Task Initialize()
        {
            ConnectEnabled = true;
        }

        private string _printerName;
        public string PrinterName
        {
            get => _printerName;
            set => SetProperty(ref _printerName, value);
        }

        private string _printerIP;
        public string PrinterIP
        {
            get => _printerIP;
            set => SetProperty(ref _printerIP, value);
        }

        private bool _connectEnabled;
        public bool ConnectEnabled
        {
            get => _connectEnabled;
            set => SetProperty(ref _connectEnabled, value);
        }

        public IMvxCommand Connect
        {
            get { return new MvxCommand(ConnectInstance); }
        }

        async void ConnectInstance()
        {
            
        }

    }
}