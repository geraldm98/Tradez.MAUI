using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Kernel.Events.Abstractions;
using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.Shared.Kernel.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task DispatchEventsAsync(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var events = entity.DomainEvents.ToList();

            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent);
            }

            entity.ClearDomainEvents();
        }

        public async Task DispatchEventsAsync(IEnumerable<Entity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                await DispatchEventsAsync(entity);
            }
        }
    }
}
