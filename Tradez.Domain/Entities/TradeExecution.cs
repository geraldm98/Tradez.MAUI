using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Shared.Kernel;

using Tradez.Domain.Abstractions;
using Tradez.Domain.Errors;
using Tradez.Domain.Events;

namespace Tradez.Domain.Entities
{
    public class TradeExecution : Entity, ITradeExecution
    {
        public Guid OrderId { get; init; }
        public decimal ExecutedQuantity { get; init; }
        public decimal ExecutedPrice { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;

        public static TradeExecution Create(Guid orderId, decimal executedQuantity, decimal executedPrice)
        {
            if (executedQuantity <= 0)
                throw new InvalidTradeOrderException("Executed quantity must be greater than zero.");

            if (executedPrice <= 0)
                throw new InvalidTradeOrderException("Executed price must be greater than zero.");

            var execution = new TradeExecution()
            {
                OrderId = orderId,
                ExecutedQuantity = executedQuantity,
                ExecutedPrice = executedPrice
            };

            execution.AddDomainEvent(new TradeOrderExecuted(orderId, executedPrice, execution.Timestamp));

            return execution;
        }
    }
}
