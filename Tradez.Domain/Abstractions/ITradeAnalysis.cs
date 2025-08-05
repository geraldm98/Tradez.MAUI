using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Common.Analysis;
using Tradez.Domain.Common.Enums;

namespace Tradez.Domain.Abstractions
{
    public interface ITradeAnalysis
    {
        T GetComponentByType<T>() where T : class, ITradeAnalysisComponent;
        void SetComponentByType<T>(T component) where T : class, ITradeAnalysisComponent;
        ITradeAnalysisComponent GetComponentByType(AnalysisTypes type);
        bool HasAnalysisType(AnalysisTypes type);
        List<AnalysisTypes> IncludedAnalysisTypes { get; }
        DateTime DateAnalyzed { get; set; }
        bool IsPlaceholder { get; }
        bool IsFresh(TimeSpan maxAge);
        void UpdateTechnicalSummary(TechnicalSummary summary);
        void UpdateSentiment(SentimentScore score, SentimentBreakdown breakdown);

        TechnicalSummary TechnicalSummary { get; set; }
        SocialStats SocialStats { get; set; }
        SentimentScore SentimentScore { get; set; }
        SentimentBreakdown SentimentBreakdown { get; set; }
    }
}
