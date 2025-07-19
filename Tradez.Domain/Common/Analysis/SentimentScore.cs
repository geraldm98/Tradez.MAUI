using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Abstractions;

namespace Tradez.Domain.Common.Analysis
{
    public record SentimentScore(
        decimal Score,
        string Summary
    ) : ITradeAnalysisComponent
    {
        public static SentimentScore Default => new(0, "Unknown");
        public bool IsPlaceholder => Score == 0 && Summary == "Unknown";
    }
}
