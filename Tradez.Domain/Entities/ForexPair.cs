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
    internal class ForexPair : TradeableAsset, ITradingPair
    {
        public override AssetTypes AssetType => AssetTypes.ForexPair;
        public string BaseAsset { get; set; }
        public string QuoteAsset { get; set; }
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }

        public static CryptoPair Create(string baseAsset = "Euro", string quoteAsset = "US Dollar", string baseSymbol = "eur", string quoteSymbol = "usd")
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
