using System;
using Houston.Audio;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        public Type VolumeControlType { get; internal set; } = typeof(InMemoryVolumeControl);
        public TimeSpan ObserverDelay { get; internal set; } = TimeSpan.FromMilliseconds(50);
    }
}