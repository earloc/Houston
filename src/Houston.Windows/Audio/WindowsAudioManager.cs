using Houston.Audio;

namespace Houston.Audio.Windows
{
    public class WindowsAudioManager : IAudioManager
    {
        public bool IsMasterMuted {
            get => NativeAudioManager.GetMasterVolumeMute();
            set => NativeAudioManager.SetMasterVolumeMute(value);
        }

        float MasterVolume { 
            get => NativeAudioManager.GetMasterVolume(); 
            set => NativeAudioManager.SetMasterVolume(value)
        }

    }
}