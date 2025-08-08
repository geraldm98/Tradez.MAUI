using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Kernel.Events.Abstractions;

namespace Tradez.Shared.Kernel
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; }
        public bool IsDeleted { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete() => IsDeleted = true;
        public void AddDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
