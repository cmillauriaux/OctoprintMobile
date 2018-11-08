using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class SlicingProgressMessage : MvxMessage
    {
        public SlicingProgressMessage(object sender, SlicingProgressState message)
    : base(sender)
        {
            Message = message;
        }

        public SlicingProgressState Message
        {
            get;
            private set;
        }
    }
}
