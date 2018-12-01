using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using OctoPrintCore.Services;
using System;
using System.Threading.Tasks;

namespace OctoPrintCore.ViewModels
{
    public class ConnectionViewModel : MvxViewModel
    {
        private readonly IOctoService _octoService;

        private readonly IMvxNavigationService _navigationService;

        public ConnectionViewModel(IOctoService octoService, IMvxNavigationService navigationService)
        {
            _octoService = octoService;
            _navigationService = navigationService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            this.LoadUserProperties();
            ConnectEnabled = true;
            ShowDirectConnection = true;
            ShowRemoteConnection = false;
        }

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            if (!string.IsNullOrEmpty(UserProperties.GetLocalIP()) && !string.IsNullOrEmpty(UserProperties.GetApiKey()))
            {
                await _navigationService.Navigate<MainViewModel>();
            }
        }

        private string _mailRemote;
        public string MailRemote
        {
            get => _mailRemote;
            set
            {
                _mailRemote = value;
                RaisePropertyChanged(() => MailRemote);
            }
        }

        private string _passwordRemote;
        public string PasswordRemote
        {
            get => _passwordRemote;
            set
            {
                _passwordRemote = value;
                RaisePropertyChanged(() => PasswordRemote);
            }
        }

        private bool _showRemoteConnection;
        public bool ShowRemoteConnection
        {
            get => _showRemoteConnection;
            set
            {
                _showRemoteConnection = value;
                RaisePropertyChanged(() => ShowRemoteConnection);
            }
        }

        private bool _showDirectConnection;
        public bool ShowDirectConnection
        {
            get => _showDirectConnection;
            set
            {
                _showDirectConnection = value;
                RaisePropertyChanged(() => ShowDirectConnection);
            }
        }

        private string _local_ip;
        public string LocalIP
        {
            get => _local_ip;
            set
            {
                _local_ip = value;
                RaisePropertyChanged(() => LocalIP);
            }
        }

        private string _remote_ip;
        public string RemoteIP
        {
            get => _remote_ip;
            set
            {
                _remote_ip = value;
                RaisePropertyChanged(() => RemoteIP);
            }
        }

        private string _user;
        public string User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private string _api_key;
        public string APIKey
        {
            get => _api_key;
            set
            {
                _api_key = value;
                RaisePropertyChanged(() => APIKey);
            }
        }

        private bool _show_loading;
        public bool ShowLoading
        {
            get => _show_loading;
            set
            {
                _show_loading = value;
                RaisePropertyChanged(() => ShowLoading);
            }
        }

        private bool _show_error;
        public bool ShowError
        {
            get => _show_error;
            set
            {
                _show_error = value;
                RaisePropertyChanged(() => ShowError);
            }
        }

        private bool _connect_enabled;
        public bool ConnectEnabled
        {
            get => _connect_enabled;
            set
            {
                _connect_enabled = value;
                RaisePropertyChanged(() => ConnectEnabled);
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public IMvxCommand Connect
        {
            get { return new MvxCommand(ConnectUser); }
        }

        public IMvxCommand TakeQrCode
        {
            get { return new MvxCommand(ReadQrCode); }
        }

        async void ReadQrCode()
        {
            IQrCodeReader reader = new QrCodeReader();
            string code = await reader.ScanAsync();
            if (!string.IsNullOrEmpty(code))
            {
                APIKey = code;
            }
        }

            private void LoadUserProperties()
        {
            this.APIKey = UserProperties.GetApiKey();
            this.LocalIP = UserProperties.GetLocalIP();
            this.RemoteIP = UserProperties.GetRemoteIP();
            this.User = UserProperties.GetUser();
            this.Password = UserProperties.GetPassword();
        }

        async void ConnectUser()
        {
            // Hide errors
            this.HideError();

            // Display loading
            this.DisplayLoading();

            // Save user properties
            UserProperties.SetApiKey(this.APIKey);
            UserProperties.SetLocalIP(this.LocalIP);
            UserProperties.SetRemoteIP(this.RemoteIP);
            UserProperties.SetUser(this.User);
            UserProperties.SetPassword(this.Password);

            // Try to connect
            await Login();

            // Hide loading
            this.HideLoading();
        }

        private async Task Login()
        {
            try
            {

                var login = await _octoService.LoginAsync(new Model.LoginRequest() { user = this.User, pass = this.Password, remember = true });
                if (login != null && !string.IsNullOrEmpty(login.apikey))
                {
                    UserProperties.SetApiKey(login.apikey);
                    await _navigationService.Navigate<MainViewModel>();
                }
                else
                {
                    this.DisplayError("User or password are not correct");
                }
            }
            catch (Exception e)
            {
                this.DisplayError("Cannot communicate with Octoprint, please check informations : " + e);
                Console.WriteLine("ERROR : " + e);
            }
        }

        private void DisplayError(string error)
        {
            this.ShowError = true;
            this.Error = error;
        }

        private void HideError()
        {
            this.ShowError = false;
        }

        private void DisplayLoading()
        {
            this.ShowLoading = true;
            this.ConnectEnabled = false;
        }

        private void HideLoading()
        {
            this.ShowLoading = false;
            this.ConnectEnabled = true;
        }

    }
}
