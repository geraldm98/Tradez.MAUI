using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Shared.Exceptions;
using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.Shared.Messaging.Pipeline
{
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                // handle domain-specific exceptions (e.g., log or wrap)
                throw;
            }
            catch (Exception ex)
            {
                // wrap or log unexpected errors
                throw new ApplicationException($"Unhandled exception for {typeof(TRequest).Name}", ex);
            }
        }
    }
}
