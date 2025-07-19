using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Abstractions
{
    internal interface ITradingPair
    {
        string BaseAsset { get; }
        string QuoteAsset { get; }
        string BaseSymbol { get; }
        string QuoteSymbol { get; } 

    }
}
