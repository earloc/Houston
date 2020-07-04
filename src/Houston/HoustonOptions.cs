using Houston.Audio;
using Houston.Speech;
using Houston.System;
using System;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        public Type VolumeControlType { get; internal set; } = typeof(InMemoryVolumeControl);

        public Type MachineType { get; internal set; } = typeof(InMemoryMachine);

        public Type VoiceType { get; internal set; } = typeof(ConsoleVoice);

        public TimeSpan ObserverDelay { get; internal set; } = TimeSpan.FromMilliseconds(50);
        public string VoicePresetSource { get; internal set; } = "./VoicePresets.txt";
    }
}