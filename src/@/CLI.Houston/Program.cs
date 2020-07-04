using Houston;
using Houston.Audio.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CLI.Houston
{
    class Program
    {
        static Task Main(string[] args)
        {
            var services = new ServiceCollection();
            Configure(services);

            using var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var app = scope.ServiceProvider.GetRequiredService<App>();

            return app.RunAsync();

        }

        static void Configure(ServiceCollection services)
        {
            services.AddHouston(x => x.UseVolumeControl<WindowsVolumeControl>());

            services.AddScoped<App>();
        }
    }
}
