using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Audio
{
    public interface IVolume : IVolumeController
    {
        int MaxVolume { get; set; }
        bool IsManagingMaxVolume { get; set; }

        void Enforce();
    }
}
