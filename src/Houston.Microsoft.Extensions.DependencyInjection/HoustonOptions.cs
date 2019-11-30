using System;
using Houston.Audio;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        internal Type AudioManagerType { get; private set; } = typeof(InMemoryVolumeControl);

        public HoustonOptions AddAudioManager<T>() where T : IVolumeControl {
            AudioManagerType = typeof(T);
            return this;
        }

    }
}