using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tradez.Shared.Messaging.Abstractions
{
    public interface IRequest<TResponse> { }

    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public interface IRequestContext
    {
        string UserId { get; }
        string CorrelationId { get; }
        string Source { get; }
        IDictionary<string, object> Items { get; }
    }

    public interface INotification { }

    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }

    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken);
    }

    public interface IMessageEnvelope
    {
        Guid Id { get; }
        string Type { get; }
        string Body { get; }
        IDictionary<string, string> Headers { get; }
    }

    public class MessageHeaders : Dictionary<string, string>
    {
        public const string CorrelationId = "X-Correlation-ID";
        public const string MessageType = "X-Message-Type";
        public const string Timestamp = "X-Timestamp";
    }

    public interface IMessageMetadata
    {
        string CorrelationId { get; }
        string Source { get; }
        DateTime Timestamp { get; }
    }

    public interface IScheduledMessage<TResponse> : IRequest<TResponse>
    {
        DateTime ScheduledTime { get; }
    }

    public interface IMessageTracker
    {
        Task<bool> HasBeenHandledAsync(Guid messageId);
        Task MarkAsHandledAsync(Guid messageId);
    }

    public interface IExceptionHandlingBehavior<in TRequest, TResponse>
    {
        Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next,
            CancellationToken cancellationToken);
    }

    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
    }
}
