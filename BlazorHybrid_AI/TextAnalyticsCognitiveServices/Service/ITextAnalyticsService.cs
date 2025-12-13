using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyticsCognitiveServices.Models;


namespace TextAnalyticsCognitiveServices.Service
{
    public interface ITextAnalyticsService
    {
        Task<SentimentResultDto> AnalyzeSentimentAsync(string text, string language = "id");
        Task<List<SentimentResultDto>> AnalyzeSentimentBatchAsync(List<string> texts, string language = "id");
    }
}
