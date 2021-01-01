using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Houston.System.Windows
{
    public class WindowsMachine : IMachine
    {
        public Task CancelShutdownAsync() => 
            Process.Start("shutdown", "-a")
            .WaitForExitAsync();

        public Task ShutdownAsync(TimeSpan @in) => 
            Process.Start("shutdown", $"/t {Math.Round(@in.TotalSeconds)} /s")
            .WaitForExitAsync();
    }
}
