using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class WebSocketMessage
    {
        public ConnectedState connected { get; set; }
        public CurrentOrHistoryState current { get; set; }
        public CurrentOrHistoryState hystory { get; set; }
        [JsonProperty(PropertyName = "event")]
        public EventState events { get; set; }
        public SlicingProgressState slicingProgress { get; set; }
    }

    public class ConnectedState
    {
        public string apikey { get; set; }
        public string version { get; set; }
        public string branch { get; set; }
        public string display_version { get; set; }
        public string plugin_hash { get; set; }
        public string config_hash { get; set; }
    }

    public class CurrentOrHistoryState
    {
        public StateMessage state { get; set; }
        public JobInformationState job { get; set; }
        public ProgressInformationState progress { get; set; }
        public float currentZ { get; set; }
        public TemperatureState offsets { get; set; }
        public string[] logs { get; set; }
        public string[] messages { get; set; }
    }

    public class TemperatureDataPointState
    {
        public long time { get; set; }
        public TemperatureDataState tool0 { get; set; }
        public TemperatureDataState tool1 { get; set; }
        public TemperatureDataState tool2 { get; set; }
        public TemperatureDataState tool3 { get; set; }
        public TemperatureDataState bed { get; set; }
    }

    public class TemperatureDataState
    {
        public int actual { get; set; }
        public int target { get; set; }
        public int offset { get; set; }
    }

    public class TemperatureState
    {
        public int? tool0 { get; set; }
        public int? tool1 { get; set; }
        public int? tool2 { get; set; }
        public int? tool3 { get; set; }
        public int? bed { get; set; }
    }

    public class StateMessage
    {
        public string text { get; set; }
        public StateFlag flags { get; set; }
    }

    public class StateFlag
    {
        public bool operational { get; set; }
        public bool paused { get; set; }
        public bool printing { get; set; }
        public bool pausing { get; set; }
        public bool cancelling { get; set; }
        public bool sdReady { get; set; }
        public bool error { get; set; }
        public bool ready { get; set; }
        public bool closedOrError { get; set; }
    }

    public class EventState
    {
        public string type { get; set; }
    }

    public class SlicingProgressState
    {
        public string slicer { get; set; }
        public string source_location { get; set; }
        public string source_path { get; set; }
        public string dest_location { get; set; }
        public string dest_path { get; set; }
        public float progress { get; set; }
    }
}