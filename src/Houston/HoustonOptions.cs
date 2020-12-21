using Houston.Audio;
using Houston.Speech;
using Houston.System;
using System;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        public Type VolumeControlType { get; internal set; }

        public Type MachineType { get; internal set; }

        public Type VoiceType { get; internal set; }

        public TimeSpan ObserverDelay { get; internal set; } = TimeSpan.FromMilliseconds(50);
        public string VoicePresetSource { get; internal set; } = "./VoicePresets.txt";
    }
}