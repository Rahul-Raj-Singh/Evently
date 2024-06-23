﻿using Evently.Common.Application.EventBus;
using MassTransit;

namespace Evently.Common.Infrastructure.EventBus;

public class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) 
        where T : IntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}
