namespace Houston.Audio {
    public class InMemoryAudioManager : IAudioManager
    {
        public float MasterVolume { get; set; }
        public bool IsMasterMuted { get; set; }
    }
}