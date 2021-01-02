using Houston.Audio;
using Houston.Speech;
using System;
using System.Threading.Tasks;

namespace CLI.Houston
{
    public class App
    {
        public App(IVolumeController master, VolumeLimiter obrigkeit, IVoice voice)
        {
            _Master = master;
            _Obrigkeit = obrigkeit;
            _Voice = voice;
        }

        private readonly IVolumeController _Master;
        private readonly VolumeLimiter _Obrigkeit;
        private readonly IVoice _Voice;

        public async Task RunAsync()
        {
            var run = true;
            Console.CancelKeyPress += (sender, e) => run = false;

            var limitTask = Task.Run(async () =>
            {
                while (run)
                {
                    await Task.Delay(2000).ConfigureAwait(false);
                    _Obrigkeit.Enforce();
                }
            });

            Console.Clear();
            while (run)
            {
                await Task.Delay(50).ConfigureAwait(false);
                Console.SetCursorPosition(0, 0);
                System.Console.WriteLine();
                Console.SetCursorPosition(0, 0);
                var mutedState = _Master.IsMuted ? "Off" : "On ";
                var limiterState = _Obrigkeit.IsEnabled ? $"({_Obrigkeit.MaxVolume})" : "";
                Console.WriteLine($"{mutedState} / {_Master.Current}{limiterState}");

                if (!Console.KeyAvailable)
                {
                    continue;
                }

                var pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.Multiply || pressed.Key == ConsoleKey.RightArrow)
                {
                    _Master.IsMuted = !_Master.IsMuted;
                }

                if (pressed.Key == ConsoleKey.Add || pressed.Key == ConsoleKey.UpArrow)
                {
                    _Master.Current = _Master.Current + 5;
                }

                if (pressed.Key == ConsoleKey.Subtract || pressed.Key == ConsoleKey.DownArrow)
                {
                    _Master.Current = _Master.Current - 5;
                }

                if (pressed.Key == ConsoleKey.Tab || pressed.Key == ConsoleKey.LeftArrow)
                {
                    _Obrigkeit.IsEnabled = !_Obrigkeit.IsEnabled;
                }

                if (pressed.Key == ConsoleKey.W)
                {
                    _Obrigkeit.MaxVolume += 5;
                }

                if (pressed.Key == ConsoleKey.S)
                {
                    _Obrigkeit.MaxVolume -= 5;
                }
            }

            await limitTask.ConfigureAwait(false);

        }
    }
}
