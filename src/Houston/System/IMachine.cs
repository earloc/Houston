using System;
using System.Threading.Tasks;

namespace Houston.System
{
    public interface IMachine
    {
        Task ShutdownAsync(TimeSpan shutdownIn);
        Task CancelShutdownAsync();
    }
}
