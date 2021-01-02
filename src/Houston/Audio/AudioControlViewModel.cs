using Houston.System;
using System;
using System.Threading.Tasks;

namespace Houston.Audio
{
    public class AudioControlViewModel : IDisposable
    {
        private readonly IVolume _Volume;
        private readonly AudioStateObserver _VolumeDetective;
        private readonly IMachine _Machine;

        public AudioControlViewModel(IVolume volume, AudioStateObserver volumeDetective, IMachine machine)
        {
            _Volume = volume;
            _VolumeDetective = volumeDetective ?? throw new ArgumentNullException(nameof(volumeDetective));
            _Machine = machine;
            _VolumeDetective.VolumeChanged += OnVolumeChanged;
            _VolumeDetective.IsMutedChanged += OnIsMutedChanged;
        }

        private void OnVolumeChanged(object? sender, ValueChangedEventArgs<int> e)
            => CurrentVolume = e.To;

        private void OnIsMutedChanged(object? sender, ValueChangedEventArgs<bool> e)
            => IsMuted = e.To;

        private decimal _CurrentVolume;
        public decimal CurrentVolume
        {
            get => _CurrentVolume;
            set
            {
                _CurrentVolume = value;
                _Volume.Current = Convert.ToInt32(value);
                OnAudioChanged();
            }
        }

        private void OnAudioChanged() => AudioChanged?.Invoke(this, EventArgs.Empty);

        public decimal LimitVolume
        {
            get
            {
                return _Volume.MaxVolume;
            }
            set
            {
                _Volume.MaxVolume = Convert.ToInt32(Math.Round(value));
                OnAudioChanged();
            }
        }

        public bool IsVolumeLimitEnabled
        {
            get => _Volume.IsManagingMaxVolume;
            set
            {
                _Volume.IsManagingMaxVolume = value;
                OnAudioChanged();
            }
        }

        public bool IsVolumeLimitDisabled => !IsVolumeLimitEnabled;


        public EventHandler? AudioChanged { get; set; }

        public int MinVolume { get; } = 0;
        public int MaxVolume { get; } = 100;

        public bool IsMuted
        {
            get => _Volume.IsMuted;
            set
            {
                _Volume.IsMuted = value;
                OnAudioChanged();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _VolumeDetective.VolumeChanged -= OnVolumeChanged;
                    _VolumeDetective.IsMutedChanged -= OnIsMutedChanged;
                }

                disposedValue = true;
            }
        }

        public Task ShutdownAsync() => _Machine.ShutdownAsync(TimeSpan.FromSeconds(60));

        public Task CancelShutdownAsync() => _Machine.CancelShutdownAsync();



    }
}