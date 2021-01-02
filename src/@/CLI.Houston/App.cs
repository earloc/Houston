using Houston.Audio;
using Houston.Speech;
using System;
using System.Threading.Tasks;

namespace CLI.Houston
{
    public class App
    {
        public App(IVolume volume, IVoice voice)
        {
            this.volume = volume;
            this.voice = voice;
        }

        private readonly IVolume volume;
        private readonly IVoice voice;

        public async Task RunAsync()
        {
            var run = true;
            Console.CancelKeyPress += (sender, e) => run = false;

            var limitTask = Task.Run(async () =>
            {
                while (run)
                {
                    await Task.Delay(2000).ConfigureAwait(false);
                    volume.Enforce();
                }
            });

            Console.Clear();
            while (run)
            {
                await Task.Delay(50).ConfigureAwait(false);
                Console.SetCursorPosition(0, 0);
                System.Console.WriteLine();
                Console.SetCursorPosition(0, 0);
                var mutedState = volume.IsMuted ? "Off" : "On ";
                var limiterState = volume.IsManagingMaxVolume? $"({volume.MaxVolume})" : "";
                Console.WriteLine($"{mutedState} / {volume.Current}{limiterState}");

                if (!Console.KeyAvailable)
                {
                    continue;
                }

                var pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.Multiply || pressed.Key == ConsoleKey.RightArrow)
                {
                    volume.IsMuted = !volume.IsMuted;
                }

                if (pressed.Key == ConsoleKey.Add || pressed.Key == ConsoleKey.UpArrow)
                {
                    volume.Current = volume.Current + 5;
                }

                if (pressed.Key == ConsoleKey.Subtract || pressed.Key == ConsoleKey.DownArrow)
                {
                    volume.Current = volume.Current - 5;
                }

                if (pressed.Key == ConsoleKey.Tab || pressed.Key == ConsoleKey.LeftArrow)
                {
                    volume.IsManagingMaxVolume = !volume.IsManagingMaxVolume;
                }

                if (pressed.Key == ConsoleKey.W)
                {
                    volume.MaxVolume += 5;
                }

                if (pressed.Key == ConsoleKey.S)
                {
                    volume.MaxVolume -= 5;
                }
            }

            await limitTask.ConfigureAwait(false);

        }
    }
}
