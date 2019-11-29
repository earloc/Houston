
namespace Houston.Audio
{
    public static class IVolumeLimiterExtensions
    {
        public static void EnforceLimit(this IVolumeLimiter that, IAudioManager audioManager)
        {
            var maxVolume = that.MaxVolume;
            var currentVolume = audioManager.MasterVolume;

            if (maxVolume < currentVolume)
            {
                audioManager.MasterVolume = maxVolume;
            }
        }
    }

}