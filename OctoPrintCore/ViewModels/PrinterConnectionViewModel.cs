using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using OctoPrintCore.Model;
using OctoPrintCore.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OctoPrintCore.ViewModels
{
    public class PrinterConnectionViewModel : MvxViewModel
    {
        private readonly IOctoService _octoService;

        private readonly MvxSubscriptionToken _token_currrent_message;

        private readonly IMvxNavigationService _navigationService;

        public PrinterConnectionViewModel(IOctoService octoService, IMvxMessenger messenger, IMvxNavigationService navigationService)
        {
            _octoService = octoService;
            _token_currrent_message = messenger.Subscribe<CurrentMessage>(OnCurrentMessage);
            _navigationService = navigationService;
            ConnectEnabled = true;
            DisconnectEnabled = true;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.GetConnectionSettings();
        }

        private string _port_selected;
        public string PortSelected
        {
            get => _port_selected;
            set
            {
                _port_selected = value;
                RaisePropertyChanged(() => PortSelected);
            }
        }

        private IList<string> _ports;
        public IList<string> Ports
        {
            get => _ports;
            set
            {
                _ports = value;
                RaisePropertyChanged(() => Ports);
            }
        }

        private int _baudrate_selected;
        public int BaudRateSelected
        {
            get => _baudrate_selected;
            set
            {
                _baudrate_selected = value;
                RaisePropertyChanged(() => BaudRateSelected);
            }
        }

        private IList<int> _baudrates;
        public IList<int> Baudrates
        {
            get => _baudrates;
            set
            {
                _baudrates = value;
                RaisePropertyChanged(() => Baudrates);
            }
        }

        private Profile _profile_selected;
        public Profile ProfileSelected
        {
            get => _profile_selected;
            set
            {
                _profile_selected = value;
                RaisePropertyChanged(() => ProfileSelected);
            }
        }

        private IList<Profile> _profiles;
        public IList<Profile> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
                RaisePropertyChanged(() => Profiles);
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

        private bool _connect_visible;
        public bool ConnectVisible
        {
            get => _connect_visible;
            set
            {
                _connect_visible = value;
                RaisePropertyChanged(() => ConnectVisible);
            }
        }

        private bool _disconnect_visible;
        public bool DisconnectVisible
        {
            get => _disconnect_visible;
            set
            {
                _disconnect_visible = value;
                RaisePropertyChanged(() => DisconnectVisible);
            }
        }

        public IMvxCommand DisconnectAccount
        {
            get { return new MvxCommand(DisconnectAccountCommand); }
        }

        public void DisconnectAccountCommand()
        {
            UserProperties.Clear();
            this._navigationService.Navigate<ConnectionViewModel>();
        }

        private async void GetConnectionSettings()
        {
            var connectionSettings = await this._octoService.GetConnectionAsync();
            if (connectionSettings != null)
            {
                // Port setup
                this.Ports = connectionSettings.options.ports;
                if (string.IsNullOrEmpty(connectionSettings.current.port))
                {
                    this.PortSelected = connectionSettings.options.portPreference;
                } else
                {
                    this.PortSelected = connectionSettings.current.port;
                }

                // Baudrates setup
                this.Baudrates = connectionSettings.options.baudrates;
                if (connectionSettings.current.baudrate == null)
                {
                    this.BaudRateSelected = connectionSettings.options.baudratePreference.GetValueOrDefault();
                }
                else
                {
                    this.BaudRateSelected = connectionSettings.current.baudrate.GetValueOrDefault();
                }

                // Profiles setup
                this.Profiles = connectionSettings.options.printerProfiles;
                if (string.IsNullOrEmpty(connectionSettings.current.printerProfile))
                {
                    foreach (var profile in Profiles)
                    {
                        if (profile.id == connectionSettings.options.printerProfilePreference)
                        {
                            this.ProfileSelected = profile;
                        }
                    }
                }
                else
                {
                    foreach (var profile in Profiles)
                    {
                        if (profile.id == connectionSettings.current.printerProfile)
                        {
                            this.ProfileSelected = profile;
                        }
                    }
                }
            }
        }

        private void OnCurrentMessage(CurrentMessage currentMessage)
        {
            if (currentMessage.Message != null && currentMessage.Message.state != null && currentMessage.Message.state.flags != null)
            {
                if (currentMessage.Message.state.flags.closedOrError || currentMessage.Message.state.flags.error)
                {
                    DisconnectVisible = false;
                    ConnectVisible = true;
                    DisconnectEnabled = false;
                    ConnectEnabled = true;
                }
                else
                {
                    DisconnectVisible = true;
                    ConnectVisible = false;
                    DisconnectEnabled = true;
                    ConnectEnabled = false;
                }
            }
        }
    }
}
