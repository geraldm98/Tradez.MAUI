using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.Enums;
using Tradez.Domain.Entities;
using Tradez.Shared.DependencyInjection;
using Tradez.Shared.Kernel.Events;
using Tradez.Shared.Kernel.Events.Abstractions;
using Tradez.Shared.Messaging.Abstractions;
using Tradez.Shared.Messaging.Context;
using Tradez.UseCases.Events.Handlers;
using Xunit;

namespace Tradez.Tests.Domain.Dispatching
{
    public class DomainEventDispatcherTests
    {
        private readonly IMediator _mediator;
        private readonly IDomainEventDispatcher _dispatcher;

        public DomainEventDispatcherTests()
        {
            // If using MediatR, replace with your custom mediator setup
            var services = new ServiceCollection();
            services.AddMediator(typeof(TradeOrderPlacedHandler).Assembly);
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IRequestContextAccessor, RequestContextAccessor>();

            var provider = services.BuildServiceProvider();
            _mediator = provider.GetRequiredService<IMediator>();
            _dispatcher = provider.GetRequiredService<IDomainEventDispatcher>();
        }

        [Fact]
        public async Task TradeOrderPlaced_Event_Should_Dispatch_Through_Mediator()
        {
            // Arrange
            var order = TradeOrder.Create(
                Guid.NewGuid(),
                TradeTypes.Buy,
                "BTC",
                1.5m,
                "owner123",
                limitPrice: 30000m);

            Assert.Single(order.DomainEvents); // ensure event exists before dispatch

            // Act
            await _dispatcher.DispatchEventsAsync(order);

            // Assert
            Assert.Empty(order.DomainEvents); // should be cleared after dispatch
        }
    }

}
