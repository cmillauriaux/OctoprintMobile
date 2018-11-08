using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class LoginResponse
    {
        public bool active { get; set; }
        public bool admin { get; set; }
        public bool user { get; set; }
        public string apikey { get; set; }
        public string name { get; set; }
    }
}
