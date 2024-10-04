using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;

public class User : Entity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set;}
    public string LastName { get; private set;}
    public string Email { get; private set;}
    public string IdentityId { get; private set;}
    private readonly List<Role> _roles = [];
    public List<Role> Roles => _roles;

    public static User Create(string firstName, string lastName, string email, string identityId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            IdentityId = identityId
        };

        user._roles.Add(Role.Member);

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }
    
    public void Update(string firstName, string lastName)
    {
        if (firstName == FirstName && lastName == LastName) return;

        FirstName = firstName;
        LastName = lastName;

        Raise(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName));
    }

}
