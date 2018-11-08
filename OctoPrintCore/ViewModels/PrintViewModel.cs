using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using OctoPrintCore.Model;
using OctoPrintCore.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OctoPrintCore.ViewModels
{
    public class PrintViewModel : MvxViewModel
    {
        private readonly IOctoService _octoService;

        private readonly MvxSubscriptionToken _token_currrent_message;

        public PrintViewModel(IOctoService octoService, IMvxMessenger messenger)
        {
            _octoService = octoService;
            _token_currrent_message = messenger.Subscribe<CurrentMessage>(OnCurrentMessage);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            GetJob();
            GetConnexionSettings();
        }

        private string _state;
        public string State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged(() => State);
            }
        }

        private string _file;
        public string File
        {
            get => _file;
            set
            {
                _file = value;
                RaisePropertyChanged(() => File);
            }
        }

        private string _print_time;
        public string PrintTime
        {
            get => _print_time;
            set
            {
                _print_time = value;
                RaisePropertyChanged(() => PrintTime);
            }
        }

        private string _filament_length;
        public string FilamentLength
        {
            get => _filament_length;
            set
            {
                _filament_length = value;
                RaisePropertyChanged(() => FilamentLength);
            }
        }

        private string _filament_weight;
        public string FilamentWeight
        {
            get => _filament_weight;
            set
            {
                _filament_weight = value;
                RaisePropertyChanged(() => FilamentWeight);
            }
        }

        private string _print_completion;
        public string PrintCompletion
        {
            get => _print_completion;
            set
            {
                _print_completion = value;
                RaisePropertyChanged(() => PrintCompletion);
            }
        }

        private bool _is_refreshing;
        public bool IsRefreshing
        {
            get => _is_refreshing;
            set
            {
                _is_refreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        private float _print_progress;
        public float PrintProgress
        {
            get => _print_progress;
            set
            {
                _print_progress = value;
                RaisePropertyChanged(() => PrintProgress);
            }
        }

        private async Task GetJob()
        {
            var job = await this._octoService.GetJobAsync();
            DisplayJobInformation(job.job, job.progress);
        }

        private void DisplayJobInformation(JobInformationState job, ProgressInformationState progress)
        {
            if (job != null)
            {
                File = job.file.name;
                if (job.estimatedPrintTime.Value < 60)
                {
                    PrintTime = job.estimatedPrintTime.Value.ToString("F0") + "s";
                }
                else if (job.estimatedPrintTime.Value < 600)
                {
                    PrintTime = (job.estimatedPrintTime.Value / 60).ToString("F0") + "mns";
                }
                else
                {
                    PrintTime = (job.estimatedPrintTime.Value / 3600).ToString("F0") + "h";
                }

                if (job.filament.tool0.length < 100)
                {
                    FilamentLength = job.filament.tool0.length.ToString("F0") + "cm";
                }
                else
                {
                    FilamentLength = (job.filament.tool0.length / 100).ToString("F0") + "m";
                }
                if (job.filament.tool0.volume < 1000)
                {
                    FilamentWeight = job.filament.tool0.volume.ToString("F0") + "g";
                }
                else
                {
                    FilamentWeight = (job.filament.tool0.volume / 1000).ToString("F0") + "kg";
                }
                
            }

            if (progress != null)
            {
                PrintCompletion = (progress.completion / 100).ToString("P");
                PrintProgress = progress.completion;

                if (progress.printTimeLeft < 60)
                {
                    PrintTime = progress.printTimeLeft.ToString("F0") + "s";
                }
                else if (job.estimatedPrintTime.Value < 600)
                {
                    PrintTime = (progress.printTimeLeft / 60).ToString("F0") + "mns";
                }
                else
                {
                    PrintTime = (progress.printTimeLeft / 3600).ToString("F0") + "h";
                }
            }
        }

        private async Task GetConnexionSettings()
        {
            var connexionSettings = await _octoService.GetConnectionAsync();
            if (connexionSettings != null)
            {
                State = connexionSettings.current.state;
            }
        }

        private void OnCurrentMessage(CurrentMessage currentMessage)
        {
            if (currentMessage.Message != null && currentMessage.Message.state != null && currentMessage.Message.state.flags != null)
            {
                if (currentMessage.Message.state.flags.closedOrError || currentMessage.Message.state.flags.error)
                {
                    this.State = "Printer offline";
                }
                else if (currentMessage.Message.state.flags.cancelling)
                {
                    this.State = "Canceling print";
                }
                else if (currentMessage.Message.state.flags.paused)
                {
                    this.State = "Paused print";
                }
                else if (currentMessage.Message.state.flags.pausing)
                {
                    this.State = "Pausing print";
                }
                else if (currentMessage.Message.state.flags.printing)
                {
                    this.State = "Printing";
                }
                else if (currentMessage.Message.state.flags.operational || currentMessage.Message.state.flags.ready || currentMessage.Message.state.flags.sdReady)
                {
                    this.State = "Printer online";
                }
            }
            DisplayJobInformation(currentMessage.Message.job, currentMessage.Message.progress);
        }

        private MvxCommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(DoRefreshCommand);

        private async void DoRefreshCommand()
        {
            IsRefreshing = true;
            await this.GetConnexionSettings();
            await this.GetJob();
            IsRefreshing = false;
        }
    }
}
