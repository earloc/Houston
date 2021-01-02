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
        private readonly IVolumeController control;

        public VolumeController(IVolumeController control)
        {
            this.control = control;
        }

        [HttpGet]
        public int Volume()
        {
            return control.Current;
        }

        [HttpGet("[action]")]
        public bool IsMuted()
        {
            return control.IsMuted;
        }

        [HttpPut("{volume:double?}")]
        public int Volume(double? volume)
        {
            //TODO: this should go into a handler or sth!
            control.IsMuted = false;

            if (!volume.HasValue) return control.Current;

            //TODO: handle values exceeding 0 and 100
            return control.Current = Convert.ToInt32(Math.Floor(volume.Value));
        }


        [HttpPut("[action]/{isMuted:bool}")]
        public bool IsMuted(bool isMuted)
        {
            return control.IsMuted = isMuted;
        }

        [HttpDelete]
        public bool Mute()
        {
            return control.IsMuted = true;
        }
    }
}
