using System;
using System.Threading.Tasks;

namespace Houston.Speech
{
    internal class ConsoleVoice : IVoice
    {
        public Task SayAsync(string message) => Task.Run(() => Console.WriteLine(message));
    }
}
