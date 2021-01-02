using Houston.Audio;
using Houston.Audio.Commands;
using Houston.Audio.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Houston.Audio
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumeController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IVolume volume;

        public VolumeController(IMediator mediator, IVolume volume)
        {
            this.mediator = mediator;
            this.volume = volume;
        }

        [HttpGet]
        public async Task<int> Volume() => (await mediator.Send(new GetVolumeQuery.Request())).Volume;

        [HttpGet("[action]")]
        public bool IsMuted()
        {
            return volume.IsMuted;
        }

        [HttpPut("{volume:double}")]
        public async Task<int> Volume(double volume) => (await mediator.Send(new SetVolumeCommand.Request((int)Math.Floor(volume)))).Volume;

        [HttpPut("[action]/{isMuted:bool}")]
        public Task<bool> IsMuted(bool isMuted) => MuteAsync(isMuted);

        [HttpDelete]
        public Task<bool> Mute() => MuteAsync(true);

        private async Task<bool> MuteAsync(bool isMuted) => (await mediator.Send(new SetIsMutedCommand.Request(isMuted))).IsMuted;
    }
}
