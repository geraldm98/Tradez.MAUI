using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Abstractions;
using Tradez.Domain.Common.Enums;
using Tradez.Domain.Common.ValueObjects;

namespace Tradez.Domain.Entities
{
    internal class CryptoPair : TradeableAsset, ITradingPair
    {
        public override AssetTypes AssetType => AssetTypes.CryptoPair;
        public string BaseAsset { get; set; }
        public string QuoteAsset { get; set; }
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }

        public static CryptoPair Create(string baseAsset, string quoteAsset, string baseSymbol, string quoteSymbol)
        {
            return new()
            {
                BaseSymbol = baseSymbol.ToUpper(),
                QuoteSymbol = quoteSymbol.ToUpper(),
                BaseAsset = baseAsset,
                QuoteAsset = quoteAsset,
                Name = $"{baseAsset} to {quoteAsset}",
                Symbol = $"{baseSymbol}/{quoteSymbol}",
                Metadata = AssetMetadata.Default,
                AnalysisHistory = []
            };
        }
    }
}
