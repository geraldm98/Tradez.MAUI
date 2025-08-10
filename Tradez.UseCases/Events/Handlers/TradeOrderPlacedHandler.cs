using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Events;
using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.UseCases.Events.Handlers
{
    public class TradeOrderPlacedHandler : INotificationHandler<TradeOrderPlaced>
    {
        public Task Handle(TradeOrderPlaced notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"TradeOrderPlaced event handled for OrderId: {notification.OrderId}");
            return Task.CompletedTask;
        }
    }
}
