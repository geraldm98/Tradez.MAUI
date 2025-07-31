using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.Enums;

namespace Tradez.Domain.Entities
{
    internal class CryptoPair : TradingPair
    {
        public override AssetTypes AssetType => AssetTypes.CryptoPair;

    }
}
