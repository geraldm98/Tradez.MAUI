using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.Shared.Messaging.Context
{
    public class RequestContextAccessor : IRequestContextAccessor
    {
        private static readonly AsyncLocal<IRequestContext> _context = new();

        public IRequestContext Current => _context.Value;

        public void SetContext(IRequestContext context)
        {
            _context.Value = context;
        }
    }
}
