using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Houston.Speech
{
    public interface IPresetSource
    {
        Task<IEnumerable<string>> GetPresetsAsync();
        event EventHandler Changed;
    }
}
