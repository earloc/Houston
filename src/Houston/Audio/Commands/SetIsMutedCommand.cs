using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Audio.Commands
{
    public class SetIsMutedCommand
    {
        public record Request(bool IsMuted) : IRequest<Response>;

        public record Response (bool IsMuted);
        
        public class DefaultHandler : RequestHandler<Request, Response>
        {
            private readonly IVolume volume;

            public DefaultHandler(IVolume volume)
            {
                this.volume = volume;
            }

            protected override Response Handle(Request request)
            {
                volume.IsMuted = request.IsMuted;

                return new(volume.IsMuted);
            }
        }
    }

    

    
}
