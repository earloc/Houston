using System;

namespace Houston.Audio
{
    public class AudioControlViewModel : IDisposable
    {
        private readonly IVolumeControl _Volume;
        private readonly AudioStateObserver _VolumeDetective;

        public AudioControlViewModel(IVolumeControl volume, AudioStateObserver volumeDetective)
        {
            _Volume = volume;
            _VolumeDetective = volumeDetective;

            _VolumeDetective.VolumeChanged += OnVolumeChanged;
            _VolumeDetective.IsMutedChanged += OnIsMutedChanged;
        }

        private void OnVolumeChanged(object? sender, ValueChangedEventArgs<int> e) 
            => CurrentVolume = e.To;

        private void OnIsMutedChanged(object? sender, ValueChangedEventArgs<bool> e)
            => IsMuted = e.To;

        public decimal _CurrentVolume;
        public decimal CurrentVolume
        {
            get => _CurrentVolume;
            set {
                _CurrentVolume = value;
                _Volume.Current = Convert.ToInt32(value);
                AudioChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public EventHandler? AudioChanged { get; set; }

        public int MinVolume => 0;
        public int MaxVolume => 100;

        public bool IsMuted {
            get => _Volume.IsMuted;
            set {
                _Volume.IsMuted = value;
                AudioChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose() => Dispose(true);
        private bool disposedValue = false; // To detect redundant calls

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
    }
}