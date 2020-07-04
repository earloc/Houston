using Houston.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Houston.Speech
{
    public class FilePresetSource : IPresetSource, IDisposable
    {

        private readonly Task _Monitor;
        private readonly CancellationTokenSource _TokenSource = new CancellationTokenSource();

        private readonly FileInfo _Source;

        public FilePresetSource(FileInfo source)
        {
            _Source = source;

            var token = _TokenSource.Token;

            _Monitor = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(10000)
                        .ConfigureAwait(false);
                    Changed?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        public event EventHandler Changed;

        public Task<IEnumerable<string>> GetPresetsAsync()
        {
            try
            {
                return Task.FromResult(
                           File
                               .ReadAllLines(_Source.FullName)
                               .Select(_ => _.Expand())
                               .Where(_ => _.Length > 0)
                               .ToArray()
                               .AsEnumerable()
                   );
            }
            catch (FileNotFoundException)
            {
                return Task.FromResult(Enumerable.Empty<string>());
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _TokenSource.Dispose();
                    _Monitor.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);
        #endregion
    }
}
