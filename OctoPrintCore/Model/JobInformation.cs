using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore.Model
{
    public class JobInformation
    {
        public JobInformationState job { get; set; }
        public ProgressInformationState progress { get; set; }
    }

    public class ProgressInformationState
    {
        public float completion { get; set; }
        public int filepos { get; set; }
        public int printTime { get; set; }
        public int printTimeLeft { get; set; }
    }

    public class JobInformationState
    {
        public FileState file { get; set; }
        public float? estimatedPrintTime { get; set; }
        public int? lastPrintTime { get; set; }
        public FilamentToolState filament { get; set; }
    }

    public class FileState
    {
        public string name { get; set; }
        public string display { get; set; }
        public string path { get; set; }
        public string type { get; set; }
    }

    public class FilamentToolState
    {
        public FilamentState tool0 { get; set; }
        public FilamentState tool1 { get; set; }
        public FilamentState tool2 { get; set; }
        public FilamentState tool3 { get; set; }
    }

    public class FilamentState
    {
        public float length { get; set; }
        public float volume { get; set; }
    }
}
