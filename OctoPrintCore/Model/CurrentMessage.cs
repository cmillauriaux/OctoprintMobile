using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class CurrentMessage : MvxMessage
    {
        public CurrentMessage(object sender, CurrentOrHistoryState message)
    : base(sender)
        {
            Message = message;
        }

        public CurrentOrHistoryState Message
        {
            get;
            private set;
        }
    }
}
