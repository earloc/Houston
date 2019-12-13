using System;
using Houston.Audio;
using Houston.Speech;
using Houston.System;
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
            services.AddSingleton(typeof(IMachine), options.MachineType);
            services.AddSingleton(typeof(IVoice), options.VoiceType);

            services.AddSingleton<AudioStateObserver>();
            services.AddSingleton<VolumeLimiter>();
            services.AddSingleton<AudioControlViewModel>();

            return services;
        }
    }
}