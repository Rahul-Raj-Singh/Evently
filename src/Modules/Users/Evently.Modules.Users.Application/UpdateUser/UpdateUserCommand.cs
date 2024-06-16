using Evently.Common.Domain;
using MediatR;

namespace Evently.Modules.Users.Application.UpdateUser;

public class UpdateUserCommand : IRequest<Result>
{
    public Guid UserId { get; set;}
    public string FirstName { get; set;}
    public string LastName { get; set;}
}
