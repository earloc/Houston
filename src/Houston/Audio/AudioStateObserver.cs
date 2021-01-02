using System;
using System.Threading;
using System.Threading.Tasks;

namespace Houston.Audio
{
    public class AudioStateObserver : IAsyncDisposable
    {
        private readonly IVolume volume;
        private int lastKnownVolume = 0;
        private bool? lastKnownIsMuted;

        public AudioStateObserver(IVolume volume, HoustonOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.volume = volume;
            var delay = options.ObserverDelay;

            _Detective = Task.Run(async () =>
            {
                while (!_Run.Token.IsCancellationRequested)
                {
                    await Task.Delay(delay)
                        .ConfigureAwait(false);

                    volume.Enforce();
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
            var currentVolume = volume.Current;
            if (currentVolume != lastKnownVolume)
            {
                var args = new ValueChangedEventArgs<int>(lastKnownVolume, currentVolume);
                lastKnownVolume = currentVolume;

                VolumeChanged?.Invoke(this, args);
            }
        }

        private void NotifyWhenIsMutedChanged()
        {
            var isMuted = volume.IsMuted;

            if (isMuted != lastKnownIsMuted)
            {
                var args = new ValueChangedEventArgs<bool>(lastKnownIsMuted, isMuted);
                lastKnownIsMuted = isMuted;
                IsMutedChanged?.Invoke(this, args);
            }
        }

        public event EventHandler<ValueChangedEventArgs<int>>? VolumeChanged;

        public event EventHandler<ValueChangedEventArgs<bool>>? IsMutedChanged;

        private bool disposedValue = false;

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
