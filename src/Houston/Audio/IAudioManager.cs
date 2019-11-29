
namespace Houston.Audio
{
    public interface IAudioManager
    {
        int MasterVolume { get; set; }

        bool IsMasterMuted { get; set; }
    }

}