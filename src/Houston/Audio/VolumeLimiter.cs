
namespace Houston.Audio
{
    public class VolumeLimiter
    {
        private readonly IAudioManager _Audio;

        int? MaxVolume { get; set; } = 50;

        public VolumeLimiter(IAudioManager audio)
        {
            _Audio = audio;
        }

        public void EnforceLimit()
        {
            if (!MaxVolume.HasValue)
                return;

            var currentVolume = _Audio.MasterVolume;
            var maxVolume = MaxVolume.Value;

            if (maxVolume < currentVolume)
            {
                _Audio.MasterVolume = maxVolume;
            }
        }
    }

}