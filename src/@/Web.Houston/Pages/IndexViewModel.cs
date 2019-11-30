using Houston.Audio;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Web.Houston.Pages
{
    public class IndexViewModel : IDisposable
    {
        private readonly IVolumeControl _Volume;
        private readonly VolumeObserver _VolumeDetective;

        public IndexViewModel(IVolumeControl volume, VolumeObserver volumeDetective)
        {
            _Volume = volume;
            _VolumeDetective = volumeDetective;

            _VolumeDetective.VolumeChanged += _VolumeDetective_VolumeChanged;
        }

        private void _VolumeDetective_VolumeChanged(object? sender, VolumeChangedEventArgs e) 
            => CurrentVolume = e.To;

        public decimal _CurrentVolume;
        public decimal CurrentVolume
        {
            get => _CurrentVolume;
            set {
                _CurrentVolume = value;
                _Volume.Current = Convert.ToInt32(value);
                CurrentVolumeChanged.InvokeAsync(value);
            }
        }

        public EventCallback<decimal> CurrentVolumeChanged { get; set; }

        public int MinVolume => 0;
        public int MaxVolume => 100;

        public bool IsMuted {
            get => _Volume.IsMuted;
            set => _Volume.IsMuted = value;
        }

        public void Dispose() => Dispose(true);
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _VolumeDetective.VolumeChanged -= _VolumeDetective_VolumeChanged;
                }

                disposedValue = true;
            }

        }
    }
}