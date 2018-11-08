using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using OctoPrintCore.Model;
using OctoPrintCore.Services;
using System;
using System.Threading.Tasks;

namespace OctoPrintCore.ViewModels
{
    public class MainViewModel : MvxViewModel, IOctoPrintNotifications
    {
        private readonly IOctoService _octoService;

        private readonly IWebsocketService _websocketService;

        private readonly IMvxNavigationService _navigationService;

        private readonly MvxSubscriptionToken _token_octo_message;

        private readonly MvxSubscriptionToken _token_currrent_message;

        public MainViewModel(IOctoService octoService, IWebsocketService websocketService, IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _token_octo_message = messenger.Subscribe<OctoMessage>(OnOctoMessage);
            _token_currrent_message = messenger.Subscribe<CurrentMessage>(OnCurrentMessage);
            _octoService = octoService;
            _websocketService = websocketService;
            _navigationService = navigationService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            this.ConnectionStatus = "Déconnecté";
            this.ConnectionIcon = "ic_warning_red_dark_36dp";
            this.ConnectionIP = UserProperties.GetLocalIP();
            ManageWebsocketConnection();
        }

        private string _connection_icon;
        public string ConnectionIcon
        {
            get => _connection_icon;
            set
            {
                _connection_icon = value;
                RaisePropertyChanged(() => ConnectionIcon);
            }
        }

        private string _connection_ip;
        public string ConnectionIP
        {
            get => _connection_ip;
            set
            {
                _connection_ip = value;
                RaisePropertyChanged(() => ConnectionIP);
            }
        }

        private string _connection_status;
        public string ConnectionStatus
        {
            get => _connection_status;
            set
            {
                _connection_status = value;
                RaisePropertyChanged(() => ConnectionStatus);
            }
        }

        private void OnOctoMessage(OctoMessage octoMessage)
        {
            switch (octoMessage.Status)
            {
                case OctoMessage.ConnectedToOctoprint:
                    OnConnect();
                    break;
                case OctoMessage.DisconnectedFromOctoprint:
                    OnDisconnect();
                    break;
            }
        }

        private void OnCurrentMessage(CurrentMessage currentMessage)
        {
            if (currentMessage.Message != null && currentMessage.Message.state != null && currentMessage.Message.state.flags != null)
            {
                if (currentMessage.Message.state.flags.closedOrError || currentMessage.Message.state.flags.error)
                {
                    this.ConnectionStatus = "Printer offline";
                }
                else if (currentMessage.Message.state.flags.cancelling) {
                    this.ConnectionStatus = "Canceling print";
                }
                else if (currentMessage.Message.state.flags.paused)
                {
                    this.ConnectionStatus = "Paused print";
                }
                else if (currentMessage.Message.state.flags.pausing)
                {
                    this.ConnectionStatus = "Pausing print";
                }
                else if (currentMessage.Message.state.flags.printing)
                {
                    this.ConnectionStatus = "Printing";
                }
                else if (currentMessage.Message.state.flags.operational || currentMessage.Message.state.flags.ready || currentMessage.Message.state.flags.sdReady) {
                    this.ConnectionStatus = "Printer online";
                }
            }
        }

        public void OnConnect()
        {
            
            this.ConnectionIcon = "ic_check_circle_green_dark_36dp";
        }

        public void OnDisconnect()
        {
            this.ConnectionIcon = "ic_warning_red_dark_36dp";
        }

        private void ManageWebsocketConnection()
        {
            this._websocketService.Connect();
        }
    }
}
