using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Abstractions
{
    public interface ITradeAnalysisComponent
    {
        bool IsPlaceholder { get; }
    }
}
