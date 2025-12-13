using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyticsCognitiveServices.Models
{
    public record SentimentResultDto
    {
        public string Sentiment { get; init; } = ""; // "Positive"|"Neutral"|"Negative"
        public double PositiveScore { get; init; }
        public double NeutralScore { get; init; }
        public double NegativeScore { get; init; }
      
        public double Confidence { get; set; }
       // public List<SentenceDto> Sentences { get; init; } = new();
        public string Explanation { get; init; } = "";
    }


    public record SentenceDto
    {
        public string Text { get; init; } = "";
        public string Sentiment { get; init; } = "";
        public double PositiveScore { get; init; }
        public double NeutralScore { get; init; }
        public double NegativeScore { get; init; }
    }
}
