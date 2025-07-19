using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Abstractions;

namespace Tradez.Domain.Common.Analysis
{
    public record TechnicalSummary(
        decimal RSI,
        decimal MovingAverage,
        string Signal
    ) : ITradeAnalysisComponent
    {
        public static TechnicalSummary Default = new(0, 0, "Unknown");
        public bool IsPlaceholder => RSI == 0 && MovingAverage == 0 && Signal == "Unknown";
    }
}
