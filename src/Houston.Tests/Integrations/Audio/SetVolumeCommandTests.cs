using FluentAssertions;
using Houston.Audio.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Houston.Tests.Integrations.Audio
{
    public class SetVolumeCommandTests : IClassFixture<IntegrationFixture>
    {
        private readonly IntegrationFixture fixture;

        public SetVolumeCommandTests(IntegrationFixture fixture)
        {
            this.fixture = fixture;
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
            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(expected));
            var actual = response.Volume;

            actual.Should().Be(expected, "that´s what should be accepted and adopted");
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
            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(value));
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
            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(value));
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

            fixture.Volume.MaxVolume = maximum;
            fixture.Volume.IsManagingMaxVolume = true;

            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(value));
            var actual = response.Volume;

            var expected = maximum;

            actual.Should().Be(expected, "this should be the current maximum");
        }
    }
}
