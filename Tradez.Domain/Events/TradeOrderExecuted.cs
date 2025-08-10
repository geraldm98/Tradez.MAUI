using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Kernel.Events.Abstractions;

namespace Tradez.Domain.Events
{
    public record TradeOrderExecuted(Guid OrderId, decimal Price, DateTime ExecutedAt) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
