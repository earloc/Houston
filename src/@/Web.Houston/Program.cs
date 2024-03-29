using Houston;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Houston
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var services = host.Services.GetServices<IAsyncInitializable>();

            await Task.WhenAll(services.Select(_ => _.InitializeAsync()).ToArray())
                .ConfigureAwait(false);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseDefaultServiceProvider(x =>
            {
                x.ValidateOnBuild = true;
                x.ValidateScopes = true;
            });
    }
}
