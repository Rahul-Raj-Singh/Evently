using Evently.Common.Application.EventBus;

namespace Evently.Modules.Users.IntegrationEvents;

public class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(
        Guid id, 
        DateTime occuredOnUtc, 
        Guid userId, 
        string firstName, 
        string lastName, 
        string email) 
        : base(id, occuredOnUtc)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
