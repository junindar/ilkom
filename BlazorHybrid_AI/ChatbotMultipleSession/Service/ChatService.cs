using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ChatbotMultipleSession.Models;

namespace ChatbotMultipleSession.Service
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

    public class ChatHistoryService : IChatHistoryService
    {
        private readonly string historyFilePath;

        public ChatHistoryService()
        {
#if ANDROID || IOS
            // Untuk Blazor Hybrid (MAUI)
            historyFilePath = Path.Combine(FileSystem.AppDataDirectory, "chat_history.json");
#else
            // Untuk Blazor Server atau Desktop (Windows)
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ChatbotAI");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            historyFilePath = Path.Combine(folder, "chat_history.json");
#endif
        }
        public async Task SaveHistoryAsync(List<ChatMessage> messages)
        {
            try
            {
                var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(historyFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan riwayat chat: {ex.Message}");
            }
        }

        public async Task<List<ChatMessage>> LoadHistoryAsync()
        {
            try
            {
                if (!File.Exists(historyFilePath))
                    return new List<ChatMessage>();

                var json = await File.ReadAllTextAsync(historyFilePath);
                var messages = JsonSerializer.Deserialize<List<ChatMessage>>(json);
                return messages ?? new List<ChatMessage>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat riwayat chat: {ex.Message}");
                return new List<ChatMessage>();
            }
        }

        public void ClearHistory()
        {
            if (File.Exists(historyFilePath))
                File.Delete(historyFilePath);
        }
    }
}
