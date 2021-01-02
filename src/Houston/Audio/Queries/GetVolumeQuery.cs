using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Audio.Queries
{
    public class GetVolumeQuery
    {
        public record Request() : IRequest<Response>;

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
                return new(volume.Current);
            }
        }
    }

    

    
}
