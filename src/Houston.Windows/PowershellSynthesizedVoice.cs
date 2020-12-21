using Houston.Speech;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Windows.Speech
{
    public class PowershellSynthesizedVoice : IVoice
    {
        public Task SayAsync(string message) => SpeakAsync(message);

        private static Task SpeakAsync(string textToSpeech)
        {
            // Command to execute PS  
            return Execute($@"Add-Type -AssemblyName System.speech;  
            $speak = New-Object System.Speech.Synthesis.SpeechSynthesizer;                           
            $speak.Speak(""{textToSpeech}"");"); // Embedd text  

            Task Execute(string command)
            {
                // create a temp file with .ps1 extension  
                var cFile = Path.GetTempPath() + Guid.NewGuid() + ".ps1";

                //Write the .ps1  
                using var tw = new StreamWriter(cFile, false, Encoding.UTF8);
                tw.Write(command);

                // Setup the PS  
                var start =
                    new ProcessStartInfo()
                    {
                        FileName = "C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe",  // CHUPA MICROSOFT 02-10-2019 23:45                    
                        LoadUserProfile = false,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"-executionpolicy bypass -File {cFile}",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                //Init the Process  
                var p = Process.Start(start);

                // The wait may not work! :(  
                return Task.Run(() => p?.WaitForExit());
            }
        }
    }
}
