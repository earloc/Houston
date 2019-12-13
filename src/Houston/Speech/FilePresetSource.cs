using Houston.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Speech
{
    public class FilePresetSource : IPresetSource
    {
        private readonly FileInfo _Source;

        public FilePresetSource(FileInfo source) => this._Source = source;
        public Task<IEnumerable<string>> GetPresetsAsync()
            => Task.Run(() => File
                .ReadAllLines(_Source.FullName)
                .Select(_ => _.Expand())
                .Where(_ => _.Length > 0 )
                .ToArray()
                .AsEnumerable()
        );
    }
}
