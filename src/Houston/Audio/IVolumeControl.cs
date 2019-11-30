
namespace Houston.Audio
{
    public interface IVolumeControl
    {
        int Current { get; set; } 
        bool IsMuted { get; set; }
    }

}