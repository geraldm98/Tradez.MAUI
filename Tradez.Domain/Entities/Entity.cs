using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete() => IsDeleted = true;
    }
}
