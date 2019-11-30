using System;
using Houston.Audio;
using Microsoft.Extensions.DependencyInjection;
#nullable enable

namespace Houston
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddHouston(this ServiceCollection services, Action<HoustonOptions>? builder = null)
        {
            var options = new HoustonOptions();

            builder?.Invoke(options);

            services.AddSingleton(typeof(IVolumeControl), options.AudioManagerType);
            services.AddScoped<VolumeLimiter>();

            return services;
        }
    }
}