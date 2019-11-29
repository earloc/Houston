
namespace Houston.Audio
{
    public interface IAudioManager
    {
        float MasterVolume { get; set; }

        bool IsMasterMuted { get; set; }
    }

}