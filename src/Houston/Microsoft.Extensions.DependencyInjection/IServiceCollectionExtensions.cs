using Houston.Audio;
using Houston.Speech;
using Houston.System;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using MediatR;

namespace Houston
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHouston(this IServiceCollection services, Action<HoustonOptionsBuilder>? configure = null)
        {

            services.AddMediatR(typeof(IServiceCollectionExtensions));

            var optionsBuilder = new HoustonOptionsBuilder();
            configure?.Invoke(optionsBuilder);
            var options = optionsBuilder.Build();

            services.AddSingleton(options);

            services.AddScoped<IVolume, ManagedVolume>();
            services.AddScoped<AudioStateObserver>();

            services.AddSingleton(typeof(IVolumeController), options.VolumeControlType);
            services.AddSingleton(typeof(IMachine), options.MachineType);
            services.AddSingleton(typeof(IVoice), options.VoiceType);

            services.AddSingleton<IPresetSource, FilePresetSource>(_ => new FilePresetSource(new FileInfo(options.VoicePresetSource)));
            

            services.AddScoped<AudioControlViewModel>();
            services.AddScoped<VoiceViewModel>();

            return services;
        }
    }
}