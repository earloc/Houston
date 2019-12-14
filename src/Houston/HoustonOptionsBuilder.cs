using System;
using Houston.Audio;
using Houston.Speech;
using Houston.System;
#nullable enable

namespace Houston
{
    public class HoustonOptionsBuilder
    {

        public HoustonOptions Options { get; } = new HoustonOptions();

        public HoustonOptionsBuilder UseVolumeControl<T>() where T : IVolumeControl
        {
            Options.VolumeControlType = typeof(T);
            return this;
        }

        public HoustonOptionsBuilder UseMachine<T>() where T : IMachine
        {
            Options.MachineType = typeof(T);
            return this;
        }

        public HoustonOptionsBuilder UseVoice<T>(string presetPath) where T : IVoice
        {
            Options.VoicePresetSource = presetPath;
            Options.VoiceType = typeof(T);
            return this;
        }

        public HoustonOptionsBuilder CheckForVolumeChangesEvery(TimeSpan value) {
            Options.ObserverDelay = value;
            return this;
        }

    }
}