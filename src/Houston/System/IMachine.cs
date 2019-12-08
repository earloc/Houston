using System;
using System.Collections.Generic;
using System.Text;

namespace Houston.System
{
    public interface IMachine
    {
        void Shutdown(TimeSpan @in);
        void CancelShutdown();
    }
}
