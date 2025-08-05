using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Abstractions;
using Tradez.Domain.Common.Enums;

namespace Tradez.Domain.Common.Analysis
{
    public class TradeAnalysis : ITradeAnalysis
    {
        public static TradeAnalysis CreateEmpty()
        {
            return new()
            {
                DateAnalyzed = DateTime.UtcNow,
                TechnicalSummary = TechnicalSummary.Default,
                SocialStats = SocialStats.Default,
                SentimentScore = SentimentScore.Default,
                SentimentBreakdown = SentimentBreakdown.Default,
            };
        }
        public bool HasAnalysisType(AnalysisTypes type) => IncludedAnalysisTypes.Contains(type);

        public T GetComponentByType<T>() where T : class, ITradeAnalysisComponent
        {
            return typeof(T) switch
            {
                var t when t == typeof(TechnicalSummary) => TechnicalSummary as T,
                var t when t == typeof(SentimentScore) => SentimentScore as T,
                var t when t == typeof(SentimentBreakdown) => SentimentBreakdown as T,
                var t when t == typeof(SocialStats) => SocialStats as T,
                _ => null
            };
        }

        public ITradeAnalysisComponent GetComponentByType(AnalysisTypes type) => type switch
        {
            AnalysisTypes.Technical => TechnicalSummary,
            AnalysisTypes.SentimentScore => SentimentScore,
            AnalysisTypes.SentimentBreakdown => SentimentBreakdown,
            AnalysisTypes.SocialStats => SocialStats,
            _ => null
        };

        public void SetComponentByType<T>(T component) where T : class, ITradeAnalysisComponent
        {
            var matchingProperty = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.PropertyType == typeof(T) && p.CanWrite);

            if (matchingProperty is null)
                throw new InvalidOperationException($"No writable property found for type {typeof(T).Name} on {GetType().Name}.");

            matchingProperty.SetValue(this, component);
        }

        public List<AnalysisTypes> IncludedAnalysisTypes => new List<AnalysisTypes>()
        {
            TechnicalSummary != null ? AnalysisTypes.Technical : 0,
            SentimentScore != null ? AnalysisTypes.SentimentScore : 0,
            SentimentBreakdown != null ? AnalysisTypes.SentimentBreakdown : 0,
            SocialStats != null ? AnalysisTypes.SocialStats : 0
        }.Where(t => t != 0).ToList();

        public bool IsPlaceholder =>
            TechnicalSummary?.IsPlaceholder != false ||
            SentimentScore?.IsPlaceholder != false ||
            SentimentBreakdown?.IsPlaceholder != false;

        public bool IsFresh(TimeSpan maxAge)
        {
            return (DateTime.UtcNow - DateAnalyzed) <= maxAge;
        }

        public void UpdateTechnicalSummary(TechnicalSummary summary)
        {
            TechnicalSummary = summary;
        }

        public void UpdateSentiment(SentimentScore score, SentimentBreakdown breakdown)
        {
            SentimentScore = score;
            SentimentBreakdown = breakdown;
        }

        public DateTime DateAnalyzed { get; set; }
        public TechnicalSummary TechnicalSummary { get; set; }
        public SocialStats SocialStats { get; set; }
        public SentimentScore SentimentScore { get; set; }
        public SentimentBreakdown SentimentBreakdown { get; set; }
    }

}
