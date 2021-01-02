﻿using System;
using System.Threading.Tasks;

namespace Houston.Speech.Mocks
{
    public class ConsoleVoice : IVoice
    {
        public Task SayAsync(string message) => Task.Run(() => Console.WriteLine(message));
    }
}
