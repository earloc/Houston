using System;
using Houston.Audio;
using Microsoft.Extensions.DependencyInjection;
#nullable enable

namespace Houston
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHouston(this IServiceCollection services, Action<HoustonOptionsBuilder>? configure = null)
        {
            var builder = new HoustonOptionsBuilder();
            configure?.Invoke(builder);
            var options = builder.Options;

            services.AddSingleton(options);

            services.AddSingleton(typeof(IVolumeControl), options.VolumeControlType);

            services.AddScoped<VolumeObserver>();
            services.AddScoped<VolumeLimiter>();

            return services;
        }
    }
}