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
        public Guid OrderId { get; set; }
        public decimal ExecutedQuantity { get; set; }
        public decimal ExecutedPrice { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
