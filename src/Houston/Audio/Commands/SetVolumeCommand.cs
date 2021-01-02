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
            private readonly IVolumeControl volumeControl;

            public DefaultHandler(IVolumeControl volumeControl)
            {
                this.volumeControl = volumeControl;
            }

            protected override Response Handle(Request request)
            {
                var desiredVolume = request.Volume switch
                {
                    > 100 => 100,
                    < 0 => 0,
                    _ => request.Volume
                };

                var actualVolume = volumeControl.Current = desiredVolume;

                return new(actualVolume);
            }
        }
    }

    

    
}
