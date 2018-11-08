using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class ConnectMessage : MvxMessage
    {
        public ConnectMessage(object sender, ConnectedState message)
    : base(sender)
        {
            Message = message;
        }

        public ConnectedState Message
        {
            get;
            private set;
        }
    }
}
