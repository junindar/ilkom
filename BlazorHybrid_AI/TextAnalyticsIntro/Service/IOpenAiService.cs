using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyticsIntro.Models;

namespace TextAnalyticsIntro.Service
{
    public interface IOpenAiService
    {
        Task<SentimentResult> AnalyzeSentimentAsync(string text);
        Task<SentimentResult> AnalyzeSentimentMultiLanguageAsync(string text);
    }
}
