using Houston.Audio;
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
        private readonly IVolumeControl control;

        public VolumeController(IVolumeControl control)
        {
            this.control = control;
        }

        [HttpGet]
        public int Volume()
        {
            return control.Current;
        }

        [HttpPut("{volume:int?}")]
        public int Volume(int? volume)
        {
            //TODO: this should go into a handler or sth!
            control.IsMuted = false;

            if (!volume.HasValue) return control.Current;

            //TODO: handle values exceeding 0 and 100
            return control.Current = volume.Value;
        }

        [HttpDelete]
        public bool Mute()
        {
            return control.IsMuted = true;
        }
    }
}
