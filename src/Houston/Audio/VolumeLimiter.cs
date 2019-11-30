
namespace Houston.Audio
{
    public class VolumeLimiter
    {
        private readonly IAudioManager _Audio;

        public int MaxVolume { get; set; } = 10;

        public bool IsEnabled {get; set;}

        public VolumeLimiter(IAudioManager audio)
        {
            _Audio = audio;
        }

        public void EnforceLimit()
        {
            if (!IsEnabled)
                return;

            var currentVolume = _Audio.MasterVolume;

            if (MaxVolume < currentVolume)
            {
                _Audio.MasterVolume = MaxVolume;
            }
        }
    }

}