using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Audio
{
    public class ManagedVolume : IVolume
    {
        private readonly IVolumeController controller;

        public ManagedVolume(IVolumeController controller)
        {
            this.controller = controller;
        }

        public bool IsManagingMaxVolume { get; set; } = false;

        public int MaxVolume { get; set; } = 100;
        public int Current { 
            get => controller.Current; 
            set => controller.Current = value; 
        }
        public bool IsMuted { 
            get => controller.IsMuted; 
            set => controller.IsMuted = value; 
        }

        public void Enforce()
        {
            if (!IsManagingMaxVolume)
            {
                return;
            }
            if (Current <= MaxVolume)
            {
                return;
            }

            Current = MaxVolume;
        }
    }
}
