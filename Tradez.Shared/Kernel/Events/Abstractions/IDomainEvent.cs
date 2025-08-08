using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.Shared.Kernel.Events.Abstractions
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
