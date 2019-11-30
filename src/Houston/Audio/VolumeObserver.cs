using Microsoft.VisualStudio.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Houston.Audio
{
    public class VolumeObserver : IAsyncDisposable
    {
        private readonly IVolumeControl _Master;
        private int _LastKnownVolume = 0;

        public VolumeObserver(IVolumeControl master, HoustonOptions options)
        {
            _Master = master;
            var delay = options.ObserverDelay;

            _Detective = Task.Run(async () =>
            {
                while (!_Run.Token.IsCancellationRequested)
                {
                    await Task.Delay(delay);
                    NotifyWhenVolumeChanged();
                }
            });
        }

        private Task _Detective;

        private CancellationTokenSource _Run = new CancellationTokenSource();

        private void NotifyWhenVolumeChanged()
        {
            var current = _Master.Current;
            if (current == _LastKnownVolume)
                return;

            var args = new VolumeChangedEventArgs(_LastKnownVolume, current);
            _LastKnownVolume = current;

            VolumeChanged?.Invoke(this, args);
        }

        public event EventHandler<VolumeChangedEventArgs>? VolumeChanged;

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
