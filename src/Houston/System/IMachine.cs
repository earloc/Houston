using System;

namespace Houston.System
{
    public interface IMachine
    {
        void Shutdown(TimeSpan shutdownIn);
        void CancelShutdown();
    }
}
