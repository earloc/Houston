namespace Houston.Audio {
    public class InMemoryAudioManager : IAudioManager
    {
        public int MasterVolume { get; set; }
        public bool IsMasterMuted { get; set; }
    }
}