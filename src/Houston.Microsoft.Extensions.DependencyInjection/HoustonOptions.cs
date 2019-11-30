using System;
using Houston.Audio;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        internal Type VolumeControlType { get; private set; } = typeof(InMemoryVolumeControl);

        public HoustonOptions AddVolumeControl<T>() where T : IVolumeControl {
            VolumeControlType = typeof(T);
            return this;
        }

    }
}