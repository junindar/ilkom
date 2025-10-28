using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ChatbotAI.Models;

namespace ChatbotAI.Service
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly string _deploymentId;
        private readonly string _apiKey;
        private readonly string _apiVersion;

      
        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            
            _endpoint = "https://xxxx.openai.azure.com/";
            _deploymentId = "xxxxx";
            _apiKey = "xxxxxx";
            _apiVersion = "xxxxx";
        }
        public async Task<string> SendMessageAsync(List<ChatMessage> messages)
        {
            try
            {
                var url = $"{_endpoint}openai/deployments/{_deploymentId}/chat/completions?api-version={_apiVersion}";

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    messages = messages.Select(m => new { role = m.Role, content = m.Content }),
                    max_tokens = 1000,
                    temperature = 0.7
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode} - {errorText}";
                }
                //Sintaks detail pada lampiran project
                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

                var completion = jsonDoc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return completion?.Trim() ?? "(no response)";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
