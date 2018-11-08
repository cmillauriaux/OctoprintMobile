using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class ConnexionRequest
    {
        public string command { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string port { get; set; }
        [JsonProperty(Required = Required.Default)]
        public int? baudrate { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string printerProfile { get; set; }
        [JsonProperty(Required = Required.Default)]
        public bool? save { get; set; }
        [JsonProperty(Required = Required.Default)]
        public bool? autoconnect { get; set; }
    }
}
