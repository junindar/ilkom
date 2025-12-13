using Azure;
using Azure.AI.TextAnalytics;
using TextAnalyticsCognitiveServices.Models;

namespace TextAnalyticsCognitiveServices.Service
{
    public class TextAnalyticsService : ITextAnalyticsService
    {
        private readonly TextAnalyticsClient _client;

        public TextAnalyticsService()
        {
            _client = new TextAnalyticsClient(
                new Uri("https://xxxx.cognitiveservices.azure.com/"),
                new AzureKeyCredential("xxxxxx")
            );
        }

        public async Task<SentimentResultDto> AnalyzeSentimentAsync(string text, string language = "id")
        {
            var options = new AnalyzeSentimentOptions
            {
                IncludeOpinionMining = true
            };

            var response = await _client.AnalyzeSentimentAsync(text, language, options);
            var doc = response.Value;

            var result = new SentimentResultDto
            {
                Sentiment = doc.Sentiment.ToString(),
                PositiveScore = doc.ConfidenceScores.Positive,
                NeutralScore = doc.ConfidenceScores.Neutral,
                NegativeScore = doc.ConfidenceScores.Negative,
                Confidence = Math.Max(doc.ConfidenceScores.Positive, Math.Max(doc.ConfidenceScores.Neutral, doc.ConfidenceScores.Negative)),
                //Sentences = doc.Sentences.Select(s => new SentenceDto
                //{
                //    Text = s.Text,
                //    Sentiment = s.Sentiment.ToString(),
                //    PositiveScore = s.ConfidenceScores.Positive,
                //    NeutralScore = s.ConfidenceScores.Neutral,
                //    NegativeScore = s.ConfidenceScores.Negative
                //}).ToList(),

                Explanation = GenerateExplanation(doc)
            };

            return result;
        }

        public async Task<List<SentimentResultDto>> AnalyzeSentimentBatchAsync(List<string> texts, string language = "id")
        {
            var options = new AnalyzeSentimentOptions
            {
                IncludeOpinionMining = true
            };

           
            var response = await _client.AnalyzeSentimentBatchAsync(texts, language, options);
            var documents = response.Value;

            var results = new List<SentimentResultDto>();

            foreach (var doc in documents)
            {
                var result = new SentimentResultDto
                {
                    Sentiment = doc.DocumentSentiment.Sentiment.ToString(),
                    PositiveScore = doc.DocumentSentiment.ConfidenceScores.Positive,
                    NeutralScore = doc.DocumentSentiment.ConfidenceScores.Neutral,
                    NegativeScore = doc.DocumentSentiment.ConfidenceScores.Negative,
                    Confidence = Math.Max(doc.DocumentSentiment.ConfidenceScores.Positive, Math.Max(doc.DocumentSentiment.ConfidenceScores.Neutral, doc.DocumentSentiment.ConfidenceScores.Negative)),
                   

                    Explanation = GenerateExplanation(doc.DocumentSentiment)
                };

                results.Add(result);
            }

            return results;
        }


        private string GenerateExplanation(DocumentSentiment doc)
        {
            var sb = new System.Text.StringBuilder();

            foreach (var sentence in doc.Sentences)
            {
                foreach (var opinion in sentence.Opinions)
                {
                    string aspect = opinion.Target.Text;
                    string aspectSentiment = opinion.Target.Sentiment.ToString().ToLower();

                    var opinionsText = string.Join(
                        " dan ",
                        opinion.Assessments.Select(a => $"{a.Text} ({a.Sentiment.ToString().ToLower()})")
                    );

                    sb.Append(
                        $"Bagian mengenai '{aspect}' memiliki sentimen {aspectSentiment} karena terdapat opini {opinionsText}. "
                    );
                }
            }

            if (sb.Length == 0)
            {
                sb.Append("Tidak ditemukan aspek atau opini spesifik. Sentimen dihitung dari keseluruhan konteks kalimat.");
            }

            return sb.ToString();
        }
    }
}
