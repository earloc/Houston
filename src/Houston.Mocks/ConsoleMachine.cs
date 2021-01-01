using System;
using System.Threading.Tasks;

namespace Houston.System.Mocks
{
    public class ConsoleMachine : IMachine
    {
        public Task CancelShutdownAsync()
        {
            Console.WriteLine(R.InMemoryMachine_CancelShutdown);
            return Task.CompletedTask;
        }

        public Task ShutdownAsync(TimeSpan shutdownIn)
        {
            Console.WriteLine(R.InMemoryMachine_ShuttingDownIn, shutdownIn);
            return Task.CompletedTask;
        }
    }
}
