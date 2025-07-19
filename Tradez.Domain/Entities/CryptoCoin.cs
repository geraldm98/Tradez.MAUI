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
    internal class CryptoCoin : TradeableAsset
    {
        public override AssetTypes AssetType => AssetTypes.CryptoCoin;

        public static CryptoCoin Create(string symbol = "btc", string name = "bitcoin")
        {
            return new CryptoCoin
            {
                Name = name,
                Symbol = symbol.ToUpper(),
                Metadata = AssetMetadata.Default,
                AnalysisHistory = []
            };
        }
    }
}
