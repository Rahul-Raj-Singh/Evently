using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Modules.Users.Application.GetUser;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.IntegrationEvents;
using MediatR;

namespace Evently.Modules.Users.Application.RegisterUser;

public class UserRegisteredDomainEventHandler(IEventBus eventBus, ISender sender) 
    : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserQuery {UserId = notification.UserId}, cancellationToken);

        if (!result.IsSuccess)
        {
            throw new EventlyException($"Unable to find user with Id: {notification.UserId}", result.Error);
        }

        await eventBus.PublishAsync(new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccuredOnUtc,
                result.Value.Id,
                result.Value.FirstName,
                result.Value.LastName,
                result.Value.Email),
            cancellationToken);
    }
}