using Houston.Audio;
using Houston.Speech;
using Houston.System;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using MediatR;
using Houston.Audio.Mocks;
using Houston.System.Mocks;
using Houston.Speech.Mocks;

namespace Houston
{
    public static class HoustonOptionsBuilderExtensions
    {
        public static HoustonOptionsBuilder UseMocks(this HoustonOptionsBuilder options)
        {
            return options
                .UseVolumeControl<InMemoryVolumeControl>()
                .UseMachine<ConsoleMachine>()
                .UseVoice<ConsoleVoice>()
            ;
        }
    }
}