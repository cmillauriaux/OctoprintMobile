using MvvmCross;
using MvvmCross.ViewModels;
using OctoPrintCore.Services;
using OctoPrintCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterType<IOctoService, OctoService>();
            Mvx.RegisterType<IWebsocketService, WebsocketService>();
            Mvx.RegisterType<IAccountService, AccountService>();

            RegisterAppStart<ConnectionViewModel>();
        }
    }
}
