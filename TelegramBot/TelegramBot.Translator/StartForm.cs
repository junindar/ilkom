using Azure.AI.Translation.Text;
using Azure;
using Newtonsoft.Json;
using System.Text;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.Translator
{
    public class StartForm : FormBase
    {
        async Task<string> translate(string text)
        {
            string key = "ApiKey";
            string endpoint = "https://api.cognitive.microsofttranslator.com";
            string location = "global";


            string route = "/translate?api-version=3.0&from=id&to=en";
            string textToTranslate = text;
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                request.Headers.Add("Ocp-Apim-Subscription-Region", location);


                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, string>>>>>(responseBody);
                return result[0]["translations"][0]["text"];

            }

        }

        async Task<string?> translateWithSdk(string text)
        {
            Uri endpoint = new("https://api.cognitive.microsofttranslator.com");
            string apiKey = "ApiKey";
           
            string result = "";
          
            TextTranslationClient client = new TextTranslationClient(new AzureKeyCredential(apiKey), endpoint);
            Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync("en", text, sourceLanguage: "id");
            IReadOnlyList<TranslatedTextItem> translations = response.Value;
            if (translations.Any())
            {
                TranslatedTextItem translation = translations.First();
                result= translation.Translations.FirstOrDefault()?.Text;
            }


            return result;


        }
        public override async Task Load(MessageResult message)
        {
            if (message.MessageText.Trim() == "")
                return;
            if (message.MessageText.ToLower() != "/start")
            {
                return;
            }
            await Device.HideReplyKeyboard(".....");
        }

        public override async Task Render(MessageResult message)
        {
            try
            {
                if (message.MessageText.ToLower() != "/start")
                {
                   // var result = await translate(message.MessageText);
                    var result = await translateWithSdk(message.MessageText);
                    await this.Device.Send(result);
                    await this.Device.Send("Ketikkan kalimat atau kata yang akan di terjemahkan");
                    return;
                }
                await this.Device.Send("Selamat datang di Ilmu Komputer Bot. Ketikkan kalimat atau kata yang akan di terjemahkan");
            }
            catch (Exception ex)
            {
                await this.Device.Send("Terjadi kesalahan sistem, silahkan hubungi admin", null);
            }
        }

    }
}
