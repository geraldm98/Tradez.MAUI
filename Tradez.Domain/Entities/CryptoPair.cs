using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.Enums;

namespace Tradez.Domain.Entities
{
    public class CryptoPair : TradingPair
    {
        public override AssetTypes AssetType => AssetTypes.CryptoPair;

        public override string Symbol => $"{BaseAsset.Symbol}/{QuoteAsset.Symbol}";
        public override string Name => $"{BaseAsset.Name} > {QuoteAsset.Name}";
    }
}
