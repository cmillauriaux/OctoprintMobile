using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoPrintCore
{
    public interface IOctoPrintNotifications
    {
        void OnConnect();

        void OnDisconnect();
    }
}