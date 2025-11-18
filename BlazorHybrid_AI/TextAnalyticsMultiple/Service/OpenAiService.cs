using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TextAnalyticsMultiple.Models;
using static System.Net.WebRequestMethods;

namespace TextAnalyticsMultiple.Service
{
    public class OpenAiService: IOpenAiService
    {
       

        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly string _deploymentId;
        private readonly string _apiKey;
        private readonly string _apiVersion;


        public OpenAiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _endpoint = "https://xxxx.openai.azure.com/";
            _deploymentId = "xxxxx";
            _apiKey = "xxxxxx";
            _apiVersion = "xxxxx";
        }

        public async Task<SentimentResult> AnalyzeSentimentAsync(string text)
        {
            var url = $"{_endpoint}openai/deployments/{_deploymentId}/chat/completions?api-version={_apiVersion}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var prompt = $@"
            You are a sentiment analysis assistant.
            Analyze the sentiment of the following text and respond ONLY in strict JSON format:
            {{
              ""Sentiment"": ""Positive|Negative|Neutral"",
              ""Confidence"": 0.0,
              ""Explanation"": ""Short reason""
            }}
            Text: ""{text}""";



          
            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful AI assistant specialized in sentiment analysis." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.0,
                max_tokens = 200
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                return new SentimentResult
                {
                    Sentiment = "InvalidFormat",
                    Explanation = errorText
                };
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

          
            var textOut = jsonDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()?
                .Trim();

            if (string.IsNullOrWhiteSpace(textOut))
                return null;


           

            try
            {
                var result = JsonSerializer.Deserialize<SentimentResult>(textOut,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch
            {
                return new SentimentResult
                {
                    Sentiment = "InvalidFormat",
                    Explanation = textOut
                };
            }

           
        }

        public async Task<SentimentResult> AnalyzeSentimentMultiLanguageAsync(string text)
        {
            var url = $"{_endpoint}openai/deployments/{_deploymentId}/chat/completions?api-version={_apiVersion}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var prompt = $@"
            You are a multilingual sentiment analysis expert.
            Your task:
            1. Detect the language of the text automatically.
            2. Determine if the sentiment is Positive, Negative, or Neutral.
            3. Estimate a confidence score (0 to 1).
            4. Give a short explanation in the same language as the text.
            Return strictly valid JSON, no extra words.

            Example format:
            {{
              ""Language"": ""Indonesian"",
              ""Sentiment"": ""Negative"",
              ""Confidence"": 0.93,
              ""Explanation"": ""Kalimat ini menunjukkan ketidakpuasan terhadap layanan pelanggan.""
            }}

            Text to analyze:""{text}""";

            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful AI assistant specialized in sentiment analysis." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.0,
                max_tokens = 200
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                return new SentimentResult
                {
                    Sentiment = "InvalidFormat",
                    Explanation = errorText
                };
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

        
            var textOut = jsonDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()?
                .Trim();

            if (string.IsNullOrWhiteSpace(textOut))
                return null;


           

            try
            {
                var result = JsonSerializer.Deserialize<SentimentResult>(textOut,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            catch
            {
                return new SentimentResult
                {
                    Sentiment = "InvalidFormat",
                    Explanation = textOut
                };
            }


        }


        public async Task<List<SentimentResult>> AnalyzeSentimentBatchAsync(List<string> texts)
        {
            var url = $"{_endpoint}openai/deployments/{_deploymentId}/chat/completions?api-version={_apiVersion}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           
            var textsJson = JsonSerializer.Serialize(texts);

            var prompt = $@"
            You are a multilingual sentiment analysis expert.
            Analyze ALL texts in the array below in ONE response.

            Task:
            1. For each text, detect the language automatically
            2. Determine sentiment: Positive, Negative, or Neutral
            3. Estimate confidence score (0 to 1)
            4. Give short explanation in the same language as the text

            Return ONLY a valid JSON array with results for ALL texts, in the same order.

            Example output format:
            [
              {{
                ""Language"": ""Indonesian"",
                ""Sentiment"": ""Negative"",
                ""Confidence"": 0.93,
                ""Explanation"": ""Menunjukkan ketidakpuasan terhadap layanan.""
              }},
              {{
                ""Language"": ""English"",
                ""Sentiment"": ""Positive"",
                ""Confidence"": 0.88,
                ""Explanation"": ""Expresses satisfaction with the product.""
              }}
            ]
            Texts to analyze:
            {textsJson}";

            var requestBody = new
            {
                messages = new[]
                {
                new { role = "system", content = "You are a helpful AI assistant specialized in batch sentiment analysis. Always return valid JSON array." },
                new { role = "user", content = prompt }
            },
                temperature = 0.0,
                max_tokens = texts.Count * 150 // Sesuaikan dengan jumlah teks
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    return texts.Select(t => new SentimentResult
                    {
                        Sentiment = "Error",
                        Explanation = $"API Error: {errorText}"
                    }).ToList();
                }

                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

                var textOut = jsonDoc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString()?
                    .Trim();

                if (string.IsNullOrWhiteSpace(textOut))
                {
                    return texts.Select(t => new SentimentResult
                    {
                        Sentiment = "Error",
                        Explanation = "Empty response from API"
                    }).ToList();
                }

                // Parse JSON array response
                var results = JsonSerializer.Deserialize<List<SentimentResult>>(textOut,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return results ?? texts.Select(t => new SentimentResult
                {
                    Sentiment = "Error",
                    Explanation = "Failed to parse response"
                }).ToList();
            }
            catch (Exception ex)
            {
                return texts.Select(t => new SentimentResult
                {
                    Sentiment = "Error",
                    Explanation = $"Exception: {ex.Message}"
                }).ToList();
            }
        }


    }
}
