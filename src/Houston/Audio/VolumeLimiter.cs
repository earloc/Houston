
namespace Houston.Audio
{
    public class VolumeLimiter
    {
        private readonly IVolumeControl _Master;

        public int MaxVolume { get; set; } = 10;

        public bool IsEnabled {get; set;}

        public VolumeLimiter(IVolumeControl master)
        {
            _Master = master;
        }

        public void EnforceLimit()
        {
            if (!IsEnabled)
                return;

            var currentVolume = _Master.Current;

            if (MaxVolume < currentVolume)
            {
                _Master.Current = MaxVolume;
            }
        }
    }

}