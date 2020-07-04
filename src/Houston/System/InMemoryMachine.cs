using System;

namespace Houston.System
{
    public class InMemoryMachine : IMachine
    {
        public void CancelShutdown()
        {
            Console.WriteLine(R.InMemoryMachine_CancelShutdown);
        }

        public void Shutdown(TimeSpan shutdownIn)
        {
            Console.WriteLine($"shutting down in {shutdownIn}");
        }
    }
}
