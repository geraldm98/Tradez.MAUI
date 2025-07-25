using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Shared.Messaging.Abstractions.Events
{
    public interface IIntegrationEvent : INotification
    {
        Guid EventId { get; }
        DateTime SentAt { get; }
    }
}
