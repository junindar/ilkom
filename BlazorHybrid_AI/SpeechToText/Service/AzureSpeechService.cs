using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Service
{
    public class AzureSpeechService : IAzureSpeechService, IDisposable
    {
        private SpeechRecognizer? _recognizer;
        private readonly string _key = "*****";
        private readonly string _region = "southeastasia";

        public bool IsListening { get; private set; }


        public event Action<string>? OnRecognizing;
        public event Action<string>? OnRecognized;
        public event Action<string>? OnError;

        private SpeechSynthesizer? synthesizer;
        public async Task StartContinuousRecognitionAsync()
        {
            if (IsListening) return;

            var config = SpeechConfig.FromSubscription(_key, _region);
            config.SpeechRecognitionLanguage = "id-ID";

            _recognizer = new SpeechRecognizer(config);

            _recognizer.Recognizing += (s, e) => OnRecognizing?.Invoke(e.Result.Text);

            _recognizer.Recognized += (s, e) => {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                    OnRecognized?.Invoke(e.Result.Text);
            };

            _recognizer.Canceled += (s, e) => OnError?.Invoke(e.Reason.ToString());

            await _recognizer.StartContinuousRecognitionAsync();
            IsListening = true;
        }

        public async Task StopContinuousRecognitionAsync()
        {
            if (_recognizer != null)
            {
                await _recognizer.StopContinuousRecognitionAsync();
                IsListening = false;
            }
        }

        public async Task<string?> SpeakAsync(string text, string voiceName)
        {
            try
            {
                var config = SpeechConfig.FromSubscription(_key, _region);
                config.SpeechSynthesisVoiceName = voiceName;

                using (synthesizer = new SpeechSynthesizer(config))
                {
                    var result = await synthesizer.SpeakTextAsync(text);

                    if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        return cancellation.ErrorDetails;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void Dispose()
        {
            _recognizer?.Dispose();
            synthesizer?.Dispose();
        }
    }
}
