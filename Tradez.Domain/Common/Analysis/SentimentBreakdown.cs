using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Abstractions;

namespace Tradez.Domain.Common.Analysis
{
    public record SentimentBreakdown(
        int PositiveMentions,
        int NegativeMentions,
        int NeutralMentions,
        List<string> RecentComments
    ) : ITradeAnalysisComponent
    {
        public static SentimentBreakdown Default => new(0, 0, 0, []);
        public bool IsPlaceholder =>
            PositiveMentions == 0 &&
            NegativeMentions == 0 &&
            NeutralMentions == 0 &&
           (RecentComments == null || RecentComments.Count == 0);
    }
}
