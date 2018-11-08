using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class LoginRequest
    {
        public string user { get; set; }
        public string pass { get; set; }
        public bool remember { get; set; }
    }
}
