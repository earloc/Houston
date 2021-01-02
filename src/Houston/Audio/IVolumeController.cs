namespace Houston.Audio
{
    public interface IVolumeController
    {
        int Current { get; set; }
        bool IsMuted { get; set; }
    }

}