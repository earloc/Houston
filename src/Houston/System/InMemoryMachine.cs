using System;
using System.Collections.Generic;
using System.Text;

namespace Houston.System
{
    public class InMemoryMachine : IMachine
    {
        public void CancelShutdown()
        {
            Console.WriteLine("canelling shutdown");
        }

        public void Shutdown(TimeSpan @in)
        {
            Console.WriteLine($"shutting down in {@in}");
        }
    }
}
