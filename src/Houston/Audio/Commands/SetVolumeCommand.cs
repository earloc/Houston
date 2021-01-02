using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Audio.Commands
{
    public class SetVolumeCommand
    {
        public record Request(int Volume) : IRequest<Response>;

        public record Response (int Volume);
        
        public class DefaultHandler : RequestHandler<Request, Response>
        {
            private readonly IVolume volume;

            public DefaultHandler(IVolume volume)
            {
                this.volume = volume;
            }

            protected override Response Handle(Request request)
            {
                volume.IsMuted = false;

                volume.Current = request.Volume;

                return new(volume.Current);
            }
        }
    }

    

    
}
