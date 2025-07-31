using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Abstractions
{
    public interface ITradeExecution
    {
        Guid OrderId { get; }
        decimal ExecutedQuantity { get; }
        decimal ExecutedPrice { get; }
        DateTime Timestamp { get; }
    }
}
