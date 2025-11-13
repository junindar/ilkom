using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyticsMultiple.Models
{
    public class SentimentResult
    {
        public string? Language { get; set; }
        public string Sentiment { get; set; }
        public double Confidence { get; set; }
        public string Explanation { get; set; }
    }
}
