using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Shared.Kernel.Events.Abstractions
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync(Entity entity);
        Task DispatchEventsAsync(IEnumerable<Entity> entities);
    }
}
