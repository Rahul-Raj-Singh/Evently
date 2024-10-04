using Evently.Common.Domain;
using MediatR;

namespace Evently.Modules.Users.Application.RegisterUser;

public class RegisterUserCommand : IRequest<Result<Guid>>
{
    public string Email { get; set;}
    public string Password { get; set;}
    public string FirstName { get; set;}
    public string LastName { get; set;}
}
