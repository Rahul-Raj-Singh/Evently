using Evently.Common.Domain;
using MediatR;

namespace Evently.Modules.Users.Application.RegisterUser;

public class RegisterUserCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; set;}
    public string LastName { get; set;}
    public string Email { get; set;}
}
