using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Abstractions;

namespace Tradez.Domain.Entities
{
    public class TradeExecution : Entity, ITradeExecution
    {
        public Guid OrderId { get; init; }
        public decimal ExecutedQuantity { get; init; }
        public decimal ExecutedPrice { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
}
