using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Shared.Messaging.Abstractions.Events;

namespace Tradez.Domain.Events
{
    public record TradeExecutionCompleted(Guid ExecutionId, Guid OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
