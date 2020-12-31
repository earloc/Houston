using Houston.Audio;
using Houston.Speech;
using Houston.System;
using System;

namespace Houston
{
    public class HoustonOptionsBuilder
    {

        private PlatformOptions platform = new ();

        public HoustonOptionsBuilder UseVolumeControl<T>() where T : IVolumeControl
        {
            platform.VolumeControlType = typeof(T);
            return this;
        }

        public HoustonOptionsBuilder UseMachine<T>() where T : IMachine
        {
            platform.MachineType = typeof(T);
            return this;
        }

        public HoustonOptions Build()
        {
            return new HoustonOptions(platform);
        }

        public HoustonOptionsBuilder UseVoice<T>() where T : IVoice
        {
            platform.VoiceType = typeof(T);
            return this;
        }

        public HoustonOptionsBuilder CheckForVolumeChangesEvery(TimeSpan value)
        {
            //Options.ObserverDelay = value;
            return this;
        }

    }
}