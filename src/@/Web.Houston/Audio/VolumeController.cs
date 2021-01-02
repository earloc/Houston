using Houston.Audio;
using Houston.Audio.Commands;
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
        public int Volume()
        {
            return volume.Current;
        }

        [HttpGet("[action]")]
        public bool IsMuted()
        {
            return volume.IsMuted;
        }

        [HttpPut("{volume:double}")]
        public async Task<int> Volume(double volume) 
        {
            var response = await mediator.Send(new SetVolumeCommand.Request((int)Math.Floor(volume)));
            return response.Volume;
        }


        [HttpPut("[action]/{isMuted:bool}")]
        public bool IsMuted(bool isMuted)
        {
            return volume.IsMuted = isMuted;
        }

        [HttpDelete]
        public bool Mute()
        {
            return volume.IsMuted = true;
        }
    }
}
