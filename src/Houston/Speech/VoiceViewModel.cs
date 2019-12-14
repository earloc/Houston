using Houston.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Speech
{
    public class VoiceViewModel
    {
        private readonly IVoice _Voice;

        public VoiceViewModel(IVoice voice)
        {
            _Voice = voice;
        }

        public Task SayAsync(string message) => _Voice.SayAsync(message);
    }
}
