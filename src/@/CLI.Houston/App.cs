using System;
using System.Threading.Tasks;
using Houston.Audio;

namespace CLI.Houston
{
    public class App
    {
        public App(IAudioManager audio)
        {
            _Audio = audio;
        }

        private readonly IAudioManager _Audio;

        public async Task RunAsync()
        {
            var run = true;
            Console.CancelKeyPress += (sender, e) => run = false;

            Console.Clear();
            while (run)
            {
                await Task.Delay(50);
                Console.SetCursorPosition(0, 0);
                var mutedState = _Audio.IsMasterMuted? "Off" : "On ";
                Console.WriteLine($"{mutedState} / {_Audio.MasterVolume}");

                if (!Console.KeyAvailable)
                {
                    continue;
                }

                var pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.Multiply) {
                    _Audio.IsMasterMuted = !_Audio.IsMasterMuted;
                }

                if (pressed.Key == ConsoleKey.Add) {
                    _Audio.MasterVolume = _Audio.MasterVolume + 5;
                }
                if (pressed.Key == ConsoleKey.Subtract) {
                    _Audio.MasterVolume = _Audio.MasterVolume - 5;
                }
            }

        }
    }
}
