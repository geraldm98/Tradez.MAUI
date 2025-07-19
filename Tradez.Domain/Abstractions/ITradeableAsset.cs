using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.Enums;
using Tradez.Domain.Common.ValueObjects;

namespace Tradez.Domain.Abstractions
{
    internal interface ITradeableAsset
    {
        string Symbol { get; }
        string Name { get; }
        AssetMetadata Metadata { get; }
        AssetTypes AssetType { get; }

        IReadOnlyList<ITradeAnalysis> AnalysisHistory { get; }
        ITradeAnalysis LatestAnalysis { get; }
        bool IsPlaceholderAnalysis { get; }
        bool IsAnalysisFresh(TimeSpan maxAge);
        void AddAnalysis(ITradeAnalysis analysis);
    }
}
