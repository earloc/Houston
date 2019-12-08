using System;
using Houston.Audio;
using Houston.System;
#nullable enable

namespace Houston
{
    public class HoustonOptions
    {
        public Type VolumeControlType { get; internal set; } = typeof(InMemoryVolumeControl);

        public Type MachineType { get; internal set; } = typeof(InMemoryMachine);

        public TimeSpan ObserverDelay { get; internal set; } = TimeSpan.FromMilliseconds(50);
    }
}