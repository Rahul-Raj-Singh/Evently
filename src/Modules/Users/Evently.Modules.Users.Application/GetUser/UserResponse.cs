namespace Evently.Modules.Users.Application.GetUser;

public sealed record UserResponse(Guid Id, string Email, string FirstName, string LastName);
