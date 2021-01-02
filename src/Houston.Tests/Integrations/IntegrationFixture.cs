using Houston.Audio;
using Houston.Audio.Mocks;
using Houston.Speech.Mocks;
using Houston.System.Mocks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Tests.Integrations
{
    public class IntegrationFixture
    {
        public IntegrationFixture()
        {
            var services = new ServiceCollection();

            services.AddHouston(options => options
                .UseMachine<ConsoleMachine>()
                .UseVoice<ConsoleVoice>()
                .UseVolumeControl<InMemoryVolumeControl>()
            );

            services.AddTransient<IVolume, ManagedVolume>();

            var provider = services.BuildServiceProvider(new ServiceProviderOptions()
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });

            Mediator = provider.GetRequiredService<IMediator>();
            Volume = provider.GetRequiredService<IVolume>();
        }

        public IMediator Mediator;
        public IVolume Volume;

    }
}
