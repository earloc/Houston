using Houston;
using Houston.Audio.Windows;
using Houston.System.Windows;
using Houston.Windows.Speech;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CLI.Houston
{
    static class Program
    {
        static Task Main()
        {
            var services = new ServiceCollection();
            Configure(services);

            using var provider = services.BuildServiceProvider(new ServiceProviderOptions()
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });

            using var scope = provider.CreateScope();
            var app = scope.ServiceProvider.GetRequiredService<App>();

            return app.RunAsync();
        }

        static void Configure(ServiceCollection services)
        {
            services.AddHouston(
                x => x
                  .UseVolumeControl<WindowsVolumeControl>()
                  .UseMachine<WindowsMachine>()
                  .UseVoice<PowershellSynthesizedVoice>()
            );

            services.AddScoped<App>();
        }
    }
}
