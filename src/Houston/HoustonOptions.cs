using Houston.Audio;
using Houston.Speech;
using Houston.System;
using System;

namespace Houston
{

    public class PlatformOptions
    {
        public Type? VolumeControlType { get; internal set; }

        public Type? MachineType { get; internal set; }

        public Type? VoiceType { get; internal set; }
    }

    public class HoustonOptions
    {
        private readonly PlatformOptions platform;

        public HoustonOptions(PlatformOptions platform)
        {
            this.platform = platform;

            VolumeControlType = platform.VolumeControlType ?? throw PlatformException(nameof(VolumeControlType));
            MachineType = platform.MachineType ?? throw PlatformException(nameof(MachineType));
            VoiceType = platform.VoiceType ?? throw PlatformException(nameof(VoiceType));
        }

        private Exception PlatformException(string missingImplementationType)
        {
            var message = string.Format(R.HoustonOptions_PlatformDoesNotSupportOrConfigure, missingImplementationType);
            return new PlatformNotSupportedException(message);
        }

        public Type VolumeControlType { get; }

        public Type MachineType { get; }

        public Type VoiceType { get; }

        public TimeSpan ObserverDelay { get; internal set; } = TimeSpan.FromMilliseconds(50);
        public string VoicePresetSource { get; internal set; } = "./VoicePresets.txt";
    }
}