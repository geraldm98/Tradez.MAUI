using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Abstractions
{
    public interface ITradingPair : ITradeableAsset
    {
        ITradeableAsset BaseAsset { get; }
        ITradeableAsset QuoteAsset { get; }
        string Pair { get; }
    }
}
