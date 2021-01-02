using FluentAssertions;
using Houston.Audio;
using Houston.Audio.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Houston.Tests.Integrations.Audio
{
    public class SetVolumeCommandTests : IDisposable
    {

        private IMediator mediator => scope.ServiceProvider.GetRequiredService<IMediator>();

        private IServiceScope scope;

        private IVolume volume => scope.ServiceProvider.GetRequiredService<IVolume>();


        public SetVolumeCommandTests()
        {
            var services = new ServiceCollection();

            services.AddHouston(options => options.UseMocks());

            var provider = services.BuildServiceProvider(new ServiceProviderOptions()
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });

            scope = provider.CreateScope();
        }

        [Theory]
        [InlineData(000)]
        [InlineData(010)]
        [InlineData(025)]
        [InlineData(050)]
        [InlineData(075)]
        [InlineData(100)]
        public async Task SetsVolume_To_Valid_Value(int expected)
        {
            var response = await mediator.Send(new SetVolumeCommand.Request(expected));
            var actual = response.Volume;

            actual.Should().Be(expected, "that´s what should be accepted and adopted");
        }

        [Theory]
        [InlineData(010)]
        [InlineData(025)]
        [InlineData(050)]
        [InlineData(075)]
        [InlineData(100)]
        public async Task SetsVolume_UnMutes_Automatically(int expected)
        {
            volume.IsMuted = true;
            var response = await mediator.Send(new SetVolumeCommand.Request(expected));

            volume.IsMuted.Should().BeFalse("setting a value above 0 should unmute the target");
        }

        [Theory]
        [InlineData(0100)]
        [InlineData(0110)]
        [InlineData(0125)]
        [InlineData(0150)]
        [InlineData(0175)]
        [InlineData(1000)]
        public async Task Limits_Volume_Exceeding_Default_UpperBounds(int value)
        {
            var response = await mediator.Send(new SetVolumeCommand.Request(value));
            var actual = response.Volume;

            var expected = 100;

            actual.Should().Be(expected, "this denotes the maximum upper bound value");
        }

        [Theory]
        [InlineData(-000)]
        [InlineData(-010)]
        [InlineData(-025)]
        [InlineData(-050)]
        [InlineData(-075)]
        [InlineData(-100)]
        public async Task Limits_Volume_Exceeding_Default_LowerBounds(int value)
        {
            var response = await mediator.Send(new SetVolumeCommand.Request(value));
            var actual = response.Volume;

            var expected = 0;

            actual.Should().Be(expected, "this denotes the minimum lower bound value");
        }

        [Theory]
        [InlineData(026, 025)]
        [InlineData(050, 025)]
        [InlineData(075, 025)]
        [InlineData(100, 025)]
        [InlineData(051, 050)]
        [InlineData(075, 050)]
        [InlineData(100, 050)]
        [InlineData(076, 075)]
        [InlineData(100, 075)]
        public async Task Limits_Volume_ToMaximum_IfEnabled(int value, int maximum)
        {

            volume.MaxVolume = maximum;
            volume.IsManagingMaxVolume = true;

            var response = await mediator.Send(new SetVolumeCommand.Request(value));
            var actual = response.Volume;

            var expected = maximum;

            actual.Should().Be(expected, "this should be the current maximum");
        }

        public void Dispose() => Dispose(true);

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            scope.Dispose();

            disposed = true;

        }
    }
}
