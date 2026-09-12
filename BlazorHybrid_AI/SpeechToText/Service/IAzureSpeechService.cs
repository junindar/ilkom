using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Service
{
    public interface IAzureSpeechService
    {
        event Action<string> OnRecognizing;
        event Action<string> OnRecognized;
        event Action<string> OnError;

        bool IsListening { get; }
        Task StartContinuousRecognitionAsync();
        Task StopContinuousRecognitionAsync();
        void Dispose();

        Task<string?> SpeakAsync(string text, string voiceName);
    }
}
