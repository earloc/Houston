using System;

namespace Houston.System
{
    public interface IMachine
    {
        void Shutdown(TimeSpan @in);
        void CancelShutdown();
    }
}
