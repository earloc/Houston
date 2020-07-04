using System;
using System.Diagnostics;

namespace Houston.System.Windows
{
    public class WindowsMachine : IMachine
    {
        public void CancelShutdown() => Process.Start("shutdown", "-a");

        public void Shutdown(TimeSpan @in)
        {
            Process.Start("shutdown", $"/t {Math.Round(@in.TotalSeconds)} /s");
        }

    }
}
