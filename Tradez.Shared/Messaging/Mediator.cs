using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.Shared.Messaging
{
    public class Mediator(IServiceProvider provider, IRequestContextAccessor contextAccessor) : IMediator
    {
        private readonly IServiceProvider _provider = provider;
        private readonly IRequestContextAccessor _contextAccessor = contextAccessor;

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _provider.GetRequiredService(handlerType);

            var behaviors = _provider.GetServices(typeof(IPipelineBehavior<,>).MakeGenericType(request.GetType(), typeof(TResponse)))
                                     .Cast<dynamic>().ToArray();

            Func<Task<TResponse>> next = () => ((dynamic)handler).Handle((dynamic)request, cancellationToken);

            foreach (var behavior in behaviors.Reverse())
            {
                var current = next;
                next = () => behavior.Handle((dynamic)request, cancellationToken, current);
            }

            return await next();
        }

        public async Task Send(IRequest request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType());
            var handler = _provider.GetService(handlerType);

            if (handler is null)
                throw new InvalidOperationException($"No handler registered for {request.GetType().Name}");

            await ((dynamic)handler).Handle((dynamic)request, cancellationToken);
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            var handlers = _provider.GetServices<INotificationHandler<TNotification>>();
            var tasks = handlers.Select(h => h.Handle(notification, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}
