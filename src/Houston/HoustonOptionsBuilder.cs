using System;
using Houston.Audio;
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

        public HoustonOptionsBuilder CheckForVolumeChangesEvery(TimeSpan value) {
            Options.ObserverDelay = value;
            return this;
        }

    }
}