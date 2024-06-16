using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;

public sealed class UserProfileUpdatedDomainEvent(Guid userId, string firstName, string lastName) : DomainEvent
{
    public Guid UserId { get; set; } = userId;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}
