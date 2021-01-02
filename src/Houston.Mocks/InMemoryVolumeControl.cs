using Houston.Audio;

namespace Houston.Audio.Mocks
{
    public class InMemoryVolumeControl : IVolumeController
    {
        public int Current { get; set; }
        public bool IsMuted { get; set; }
    }
}