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
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(75)]
        [InlineData(100)]
        public async Task SetsVolume_To_Valid_Value(int expected)
        {
            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(expected));
            var actual = response.Volume;

            actual.Should().Be(expected, "that´s what should be accepted and adopted");
        }

        [Theory]
        [InlineData(100)]
        [InlineData(110)]
        [InlineData(125)]
        [InlineData(150)]
        [InlineData(175)]
        [InlineData(1000)]
        public async Task Limits_Values_Exceeding_Default_UpperBounds(int value)
        {
            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(value));
            var actual = response.Volume;

            var expected = 100;

            actual.Should().Be(expected, "this denotes the maximum upper bound value");
        }

        [Theory]
        [InlineData(-0)]
        [InlineData(-10)]
        [InlineData(-25)]
        [InlineData(-50)]
        [InlineData(-75)]
        [InlineData(-100)]
        public async Task Limits_Values_Exceeding_Default_LowerBounds(int value)
        {
            var response = await fixture.Mediator.Send(new SetVolumeCommand.Request(value));
            var actual = response.Volume;

            var expected = 0;

            actual.Should().Be(expected, "this denotes the minimum lower bound value");
        }
    }
}
