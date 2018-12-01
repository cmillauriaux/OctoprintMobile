using MvvmCross.Commands;
using MvvmCross.ViewModels;
using OctoPrintCore.Model;
using OctoPrintCore.Services;
using System;
using System.Threading.Tasks;
using MvvmCross;

namespace OctoPrintCore.ViewModels
{
    public class AccountViewModel : MvxViewModel
    {
        private readonly IFirebaseAuth _firebaseAuth;

        private readonly IFirebaseNotificationsService _firebaseNotifications;

        private readonly IAccountService _accountService;

        public AccountViewModel(IFirebaseAuth firebaseAuth, IFirebaseNotificationsService firebaseNotifications, IAccountService accountService)
        {
            _firebaseAuth = firebaseAuth;
            _firebaseNotifications = firebaseNotifications;
            _accountService = accountService;
            Instances = new MvxObservableCollection<OctoprintInstanceViewModel>();
            ConnectEnabled = true;
            SubscribeEnabled = true;
            DisconnectEnabled = true;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.GetAccountProperties();
            OctoprintInstanceViewModel instance = Mvx.IoCConstruct<OctoprintInstanceViewModel>();
            instance.PrinterIP = "192.168.1.34";
            instance.PrinterName = "Test";
            Instances.Add(instance);
        }

        #region Attributes

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

        private bool _subscribe_enabled;

        public bool SubscribeEnabled
        {
            get => _subscribe_enabled;
            set
            {
                _subscribe_enabled = value;
                RaisePropertyChanged(() => SubscribeEnabled);
            }
        }

        private bool _disconnect_enabled;

        public bool DisconnectEnabled
        {
            get => _disconnect_enabled;
            set
            {
                _disconnect_enabled = value;
                RaisePropertyChanged(() => DisconnectEnabled);
            }
        }

        private string _mail;

        public string Mail
        {
            get => _mail;
            set
            {
                _mail = value;
                RaisePropertyChanged(() => Mail);
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

        private string _status;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        private bool _isConnected;

        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                RaisePropertyChanged(() => IsConnected);
            }
        }

        private bool _isDisconnected;

        public bool IsDisconnected
        {
            get => _isDisconnected;
            set
            {
                _isDisconnected = value;
                RaisePropertyChanged(() => IsDisconnected);
            }
        }

        private bool _isNotificationsEnabled;

        public bool IsNotificationsEnabled
        {
            get => _isNotificationsEnabled;
            set
            {
                _isNotificationsEnabled = value;
                RaisePropertyChanged(() => IsNotificationsEnabled);
            }
        }

        private MvxObservableCollection<OctoprintInstanceViewModel> _instances;

        public MvxObservableCollection<OctoprintInstanceViewModel> Instances
        {
            get => _instances;
            set
            {
                _instances = value;
                RaisePropertyChanged(() => Instances);
            }
        }

        #endregion Attributes

        #region Commands

        public IMvxCommand Connect
        {
            get { return new MvxCommand(ConnectCommandAsync); }
        }

        public async void ConnectCommandAsync()
        {
            LockButtonState();
            try
            {
                if (await _firebaseAuth.Connect(Mail, Password))
                {
                    this.Status = "Utilisateur connecté";
                }
                else
                {
                    this.Status = "Utilisateur inconnu";
                }
            }
            catch (Exception e)
            {
                this.Status = "Erreur à la connexion";
                Console.WriteLine(e);
            }
            UpdateButtonState();
        }

        public IMvxCommand Disconnect
        {
            get { return new MvxCommand(DisconnectCommand); }
        }

        public async void DisconnectCommand()
        {
            LockButtonState();
            try
            {
                await _firebaseNotifications.Unregister(await _firebaseAuth.GetToken());
                if (await _firebaseAuth.Disconnect())
                {
                    this.Status = "Utilisateur déconnecté";
                }
                else
                {
                    this.Status = "Utilisateur non déconnecté";
                }
            }
            catch (Exception e)
            {
                this.Status = "Erreur à la déconnexion";
                Console.WriteLine(e);
            }
            UpdateButtonState();
        }

        public IMvxCommand Subscribe
        {
            get { return new MvxCommand(SubscribeCommand); }
        }

        public async void SubscribeCommand()
        {
            LockButtonState();
            try
            {
                if (await _firebaseAuth.Subscribe(Mail, Password))
                {
                    this.Status = "Utilisateur inscrit";
                }
                else
                {
                    this.Status = "Utilisateur non inscrit";
                }
            }
            catch (Exception e)
            {
                this.Status = "Erreur à l'inscription";
                Console.WriteLine(e);
            }
            UpdateButtonState();
        }

        public IMvxCommand ChangeNotificationState
        {
            get { return new MvxCommand(ChangeNotificationStateCommand); }
        }

        public async void ChangeNotificationStateCommand()
        {
            if (IsNotificationsEnabled)
            {
                await _firebaseNotifications.Register(await _firebaseAuth.GetToken());
            }
            else
            {
                await _firebaseNotifications.Unregister(await _firebaseAuth.GetToken());
            }
        }

        #endregion Commands

        #region Methods

        private async void LockButtonState()
        {
            ConnectEnabled = false;
            SubscribeEnabled = false;
            DisconnectEnabled = false;
        }

        private async void UpdateButtonState()
        {
            if (await _firebaseAuth.IsConnected())
            {
                ConnectEnabled = false;
                SubscribeEnabled = false;
                DisconnectEnabled = true;
                IsDisconnected = false;
                IsConnected = true;
            }
            else
            {
                ConnectEnabled = true;
                SubscribeEnabled = true;
                DisconnectEnabled = false;
                IsDisconnected = true;
                IsConnected = false;
            }
        }

        private async void GetAccountProperties()
        {
            if (await _firebaseAuth.IsConnected())
            {
                IsConnected = true;
                IsDisconnected = false;
                this.Status = "Utilisateur connecté";
                Account account = await _accountService.GetAccount(await _firebaseAuth.GetToken());
                if (account != null)
                {
                    IsNotificationsEnabled = account.IsNotificationsEnabled;
                }
                else
                {
                    IsNotificationsEnabled = false;
                }
            }
            else
            {
                IsConnected = false;
                IsDisconnected = true;
                this.Status = "Utilisateur non nconnecté";
            }
            UpdateButtonState();
        }

        #endregion Methods
    }
}