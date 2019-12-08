using Microsoft.VisualStudio.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Houston.Audio
{
    public class AudioStateObserver : IAsyncDisposable
    {
        private readonly IVolumeControl _Master;
        private int? _LastKnownVolume = 0;
        private bool? _LastKnownIsMuted;

        public AudioStateObserver(IVolumeControl master, HoustonOptions options, VolumeLimiter limit)
        {
            _Master = master;
            var delay = options.ObserverDelay;

            _Detective = Task.Run(async () =>
            {
                while (!_Run.Token.IsCancellationRequested)
                {
                    await Task.Delay(delay);
                    if (limit.IsEnabled)
                        limit.EnforceLimit();

                    NotifyChanges();
                }
            });
        }

        private Task _Detective;

        private CancellationTokenSource _Run = new CancellationTokenSource();

        private void NotifyChanges()
        {
            NotifyWhenVolumeChanged();
            NotifyWhenIsMutedChanged();
        }



        private void NotifyWhenVolumeChanged()
        {
            var currentVolume = _Master.Current;
            if (currentVolume != _LastKnownVolume)
            {
                var args = new ValueChangedEventArgs<int>(_LastKnownVolume, currentVolume);
                _LastKnownVolume = currentVolume;

                VolumeChanged?.Invoke(this, args);
            }
        }

        private void NotifyWhenIsMutedChanged()
        {
            var isMuted = _Master.IsMuted;

            if (isMuted != _LastKnownIsMuted)
            {
                var args = new ValueChangedEventArgs<bool>(_LastKnownIsMuted, isMuted);
                IsMutedChanged?.Invoke(this, args);
            }
        }

        public event EventHandler<ValueChangedEventArgs<int>>? VolumeChanged;

        public event EventHandler<ValueChangedEventArgs<bool>>? IsMutedChanged;

        private bool disposedValue = false; // To detect redundant calls

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _Run.Cancel();
                    _Run.Dispose();

                    await (_Detective ?? Task.CompletedTask);
                }

                disposedValue = true;
            }
        }

        public Task DisposeAsync() => DisposeAsync(true);
    }
}
