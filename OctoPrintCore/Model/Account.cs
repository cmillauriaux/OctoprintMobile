using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class Account
    {
        public string Id { get; set; }
        public string FCMToken { get; set; }
        public bool IsNotificationsEnabled { get; set; }
    }
}
