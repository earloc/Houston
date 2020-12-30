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

        [HttpGet]
        public int Volume()
        {
            return 0;
        }

        [HttpPut]
        public int Volume(int volume)
        {
            return volume;
        }

    }
}
