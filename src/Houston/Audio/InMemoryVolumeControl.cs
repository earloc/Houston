namespace Houston.Audio {
    public class InMemoryVolumeControl : IVolumeControl
    {
        public int Current { get; set; }
        public bool IsMuted { get; set; }
    }
}