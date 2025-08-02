using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Abstractions;

namespace Tradez.Domain.Entities
{
    internal abstract class TradingPair : TradeableAsset, ITradingPair
    {
        public ITradeableAsset BaseAsset { get; set; } = default!;
        public ITradeableAsset QuoteAsset { get; set; } = default!;
        public override string Symbol => $"{BaseAsset.Symbol}/{QuoteAsset.Symbol}";
        public override string Name => $"{BaseAsset.Name} to {QuoteAsset.Name}";
        public string Pair => Symbol;
    }
}
