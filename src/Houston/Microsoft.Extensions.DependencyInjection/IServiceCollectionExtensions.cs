using Houston.Audio;
using Houston.Speech;
using Houston.System;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Houston
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHouston(this IServiceCollection services, Action<HoustonOptionsBuilder>? configure = null)
        {
            var optionsBuilder = new HoustonOptionsBuilder();
            configure?.Invoke(optionsBuilder);
            var options = optionsBuilder.Build();

            services.AddSingleton(options);

            services.AddSingleton(typeof(IVolumeControl), options.VolumeControlType);
            services.AddSingleton(typeof(IMachine), options.MachineType);
            services.AddSingleton(typeof(IVoice), options.VoiceType);

            services.AddSingleton<IPresetSource, FilePresetSource>(_ => new FilePresetSource(new FileInfo(options.VoicePresetSource)));
            services.AddSingleton<AudioStateObserver>();
            services.AddSingleton<VolumeLimiter>();

            services.AddScoped<AudioControlViewModel>();
            services.AddScoped<VoiceViewModel>();

            return services;
        }
    }
}