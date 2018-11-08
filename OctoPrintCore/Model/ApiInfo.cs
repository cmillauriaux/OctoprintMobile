using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class ApiInfo
    {
        public bool cookie_needed { get; set; }
        public int entropy { get; set; }
        public bool websocket { get; set; }
    }
}
