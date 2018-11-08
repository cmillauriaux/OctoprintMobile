using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class EventMessage : MvxMessage
    {
        public EventMessage(object sender, EventState message)
    : base(sender)
        {
            Message = message;
        }

        public EventState Message
        {
            get;
            private set;
        }
    }
}
