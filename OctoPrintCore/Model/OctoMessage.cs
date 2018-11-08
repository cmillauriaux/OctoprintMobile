using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class OctoMessage : MvxMessage
    {
        public const string DisconnectedFromOctoprint = "DisconnectedFromOctoprint";
        public const string ConnectedToOctoprint = "ConnectedToOctoprint";

        public OctoMessage(object sender, string status)
    : base(sender)
        {
            Status = status;
        }

        public string Status
        {
            get;
            private set;
        }
    }
}
