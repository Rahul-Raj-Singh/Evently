using Evently.Common.Domain;

namespace Evently.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(string email, string password, string firstName, string lastName);
}

