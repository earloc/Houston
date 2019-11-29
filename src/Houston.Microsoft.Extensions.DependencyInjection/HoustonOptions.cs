using System;
using Houston.Audio;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        internal Type AudioManagerType { get; private set; } = typeof(InMemoryAudioManager);

        public HoustonOptions AddAudioManager<T>() where T : IAudioManager {
            AudioManagerType = typeof(T);
            return this;
        }

    }
}