using Houston.Audio;
using Houston.Speech;
using Houston.System;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using MediatR;
using Houston.Audio.Windows;
using Houston.System.Windows;
using Houston.Windows.Speech;

namespace Houston
{
    public static class HoustonOptionsBuilderExtensions
    {
        public static HoustonOptionsBuilder UseWindows(this HoustonOptionsBuilder options)
        {
            return options
                .UseVolumeControl<WindowsVolumeControl>()
                .UseMachine<WindowsMachine>()
                .UseVoice<PowershellSynthesizedVoice>()
            ;
        }
    }
}