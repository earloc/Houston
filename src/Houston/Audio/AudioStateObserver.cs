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
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _Master = master;
            var delay = options.ObserverDelay;

            _Detective = Task.Run(async () =>
            {
                while (!_Run.Token.IsCancellationRequested)
                {
                    await Task.Delay(delay)
                        .ConfigureAwait(false);

                    limit.Enforce();
                    NotifyChanges();
                }
            });
        }

        private readonly Task _Detective;

        private readonly CancellationTokenSource _Run = new CancellationTokenSource();

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
                _LastKnownIsMuted = isMuted;
                IsMutedChanged?.Invoke(this, args);
            }
        }

        public event EventHandler<ValueChangedEventArgs<int>>? VolumeChanged;

        public event EventHandler<ValueChangedEventArgs<bool>>? IsMutedChanged;

        private bool disposedValue = false; // To detect redundant calls

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _Run.Cancel();
                    _Run.Dispose();

                    await (_Detective ?? Task.CompletedTask)
                        .ConfigureAwait(false);
                }

                disposedValue = true;
            }
        }

        public ValueTask DisposeAsync() => DisposeAsync(true);
    }
}
