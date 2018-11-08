using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class ConnexionSettings
    {
        public CurrentConnexionSettings current { get; set; }
        public OptionsConnexionSettings options { get; set; }
    }

    public class CurrentConnexionSettings
    {
        [JsonProperty(Required = Required.Default)]
        public string state { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string port { get; set; }
        [JsonProperty(Required = Required.Default)]
        public int? baudrate { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string printerProfile { get; set; }
    }

    public class OptionsConnexionSettings
    {
        [JsonProperty(Required = Required.Default)]
        public string[] ports { get; set; }
        [JsonProperty(Required = Required.Default)]
        public int[] baudrates { get; set; }
        [JsonProperty(Required = Required.Default)]
        public IList<Profile> printerProfiles { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string portPreference { get; set; }
        [JsonProperty(Required = Required.Default)]
        public int? baudratePreference { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string printerProfilePreference { get; set; }
        [JsonProperty(Required = Required.Default)]
        public bool? autoconnect { get; set; }
    }

    public class Profile
    {
        public string id { get; set; }
        public string name { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
