using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.Enums;

namespace Tradez.Domain.Entities
{
    internal class ForexPair : TradingPair
    {
        public override AssetTypes AssetType => AssetTypes.ForexPair;

        public override string Symbol => $"{BaseAsset.Symbol}/{QuoteAsset.Symbol}";
        public override string Name => $"{BaseAsset.Name} > {QuoteAsset.Name}";
    }
}
