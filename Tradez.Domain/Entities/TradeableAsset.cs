using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Shared.Kernel;

using Tradez.Domain.Abstractions;
using Tradez.Domain.Common.Enums;
using Tradez.Domain.Common.ValueObjects;

namespace Tradez.Domain.Entities
{
    public abstract class TradeableAsset : Entity, ITradeableAsset
    {
        public abstract AssetTypes AssetType { get; }
        public abstract string Symbol { get; }
        public abstract string Name { get; }
        public AssetMetadata Metadata { get; set; }
        public List<ITradeAnalysis> AnalysisHistory { get; set; } = [];
        IReadOnlyList<ITradeAnalysis> ITradeableAsset.AnalysisHistory => AnalysisHistory;

        public ITradeAnalysis LatestAnalysis => AnalysisHistory
            ?.OrderByDescending(a => a.DateAnalyzed)
            .FirstOrDefault();

        public bool IsPlaceholderAnalysis => LatestAnalysis?.IsPlaceholder == true;

        public bool IsAnalysisFresh(TimeSpan maxAge)
        {
            return LatestAnalysis?.IsFresh(maxAge) == true;
        }

        public void AddAnalysis(ITradeAnalysis analysis)
        {
            AnalysisHistory.Add(analysis);
        }
    }
}
