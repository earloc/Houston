using System;

namespace Houston.System.Mocks
{
    public class ConsoleMachine : IMachine
    {
        public void CancelShutdown()
        {
            Console.WriteLine(R.InMemoryMachine_CancelShutdown);
        }

        public void Shutdown(TimeSpan shutdownIn)
        {
            Console.WriteLine(R.InMemoryMachine_ShuttingDownIn, shutdownIn);
        }
    }
}
